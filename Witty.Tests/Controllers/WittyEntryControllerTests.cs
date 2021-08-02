using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Witty.Constants;
using Witty.Controllers;
using Witty.Models;
using Witty.Repositories;
using Witty.Tests.Builders;
using Witty.Tests.Utility;
using Witty.ViewModels;

namespace Witty.Tests.Controllers
{
    [TestFixture]
    public class WittyEntryControllerTests
    {
        WittyEntryController controller;
        Mock<WittyEntryRepository> mockRepo;

        [SetUp]
        public void SetUp()
        {
            mockRepo = new Mock<WittyEntryRepository>();
            mockRepo
                .Setup(r => r.Get(It.IsAny<string>()))
                .Returns(new WittyEntry());
            mockRepo
                .Setup(r => r.Exists(It.IsAny<string>()))
                .Returns(true);

            controller = new WittyEntryController(mockRepo.Object);
        }

        [Test]
        public void WhenHttpGetAddActionCalled_ThenShowDefaultFormViewModelProperties()
        {
            AddWittyEntryFormViewModel viewModel = UnitTestUtility
                .GetModel<AddWittyEntryFormViewModel>(controller.Add());

            assertDefaultAddFormViewModelProperties(viewModel);
        }

        [Test]
        public void GivenNullWittyEntryViewModel_WhenHttpPostAddActionCalled_ThenThrowArgumentNullException()
        {
            Assert.That(() => controller.Add(null), 
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GivenEmptyQuestion_WhenHttpPostAddActionCalled_ThenRetainFormViewModelPropertiesFromUserInput()
        {
            AddWittyEntryFormViewModel viewModel = AddWittyEntryFormViewModelBuilder
                .Default()
                .WithQuestionString(string.Empty)
                .WithResponseCategory(Messenger.Analogy)
                .WithResponse("Yes")
                .Build();
            controller.ModelState.AddModelError("QuestionString", "Required");

            AddWittyEntryFormViewModel retVm = UnitTestUtility
                .GetModel<AddWittyEntryFormViewModel>(controller.Add(viewModel));

            Assert.That(retVm.QuestionString, Is.Empty);
            Assert.That(retVm.GetCategoryTextList(),
                Is.EquivalentTo(expectedCategoryTextList()));
            Assert.That(retVm.Response, Is.EqualTo("Yes"));
            Assert.That(retVm.AddSuccessMessage, Is.Empty);
        }

        [Test]
        public void GivenEmptyResponse_WhenHttpPostAddActionCalled_ThenRetainFormViewModelPropertiesFromUserInput()
        {
            AddWittyEntryFormViewModel viewModel = AddWittyEntryFormViewModelBuilder
                .Default()
                .WithQuestionString("What?")
                .WithResponseCategory(Messenger.Analogy)
                .WithResponse(string.Empty)
                .Build();
            controller.ModelState.AddModelError("Response", "Required");

            AddWittyEntryFormViewModel retVm = UnitTestUtility
                .GetModel<AddWittyEntryFormViewModel>(controller.Add(viewModel));

            Assert.That(retVm.QuestionString, Is.EqualTo("What?"));
            Assert.That(retVm.GetCategoryTextList(),
                Is.EquivalentTo(expectedCategoryTextList()));
            Assert.That(retVm.Response, Is.Empty);
            Assert.That(retVm.AddSuccessMessage, Is.Empty);
        }

        [Test]
        public void GivenValidWittyEntryViewModelWithNewQuestion_WhenHttpPostAddActionCalled_ThenCallAddMethodInRepo()
        {
            WittyEntryController controller1;
            mockRepo
                .Setup(r => r.Exists(It.IsAny<string>()))
                .Returns(false);
            controller1 = new WittyEntryController(mockRepo.Object);

            AddWittyEntryFormViewModel viewModel = AddWittyEntryFormViewModelBuilder
                .Simple()
                .Build();

            controller1.Add(viewModel);

            mockRepo.Verify(r => r.Add(It.IsAny<WittyEntry>()));
            mockRepo.Verify(r => r.AddResponses(It.IsAny<WittyEntry>()), Times.Never);
        }

        [Test]
        public void GivenValidWittyEntryViewModel_WhenHttpPostAddActionCalled_ThenReturnDefaultFormViewModelPropertiesAndSuccessIndication()
        {
            AddWittyEntryFormViewModel viewModel = AddWittyEntryFormViewModelBuilder
                .Simple()
                .Build();

            AddWittyEntryFormViewModel returnedViewModel = UnitTestUtility
                .GetModel<AddWittyEntryFormViewModel>(controller.Add(viewModel));

            assertDefaultAddFormViewModelProperties(returnedViewModel);
            Assert.That(viewModel.AddSuccessMessage, Is.EqualTo(Messenger.AddWittyEntrySuccess));
        }

        [Test]
        public void GivenExistingQuestion_WhenHttpPostAddActionCalled_ThenCallAddResponsesMethodInRepo()
        {
            WittyEntryController controller1;
            mockRepo
                .Setup(r => r.Exists(It.IsAny<string>()))
                .Returns(true);
            controller1 = new WittyEntryController(mockRepo.Object);

            AddWittyEntryFormViewModel viewModel = AddWittyEntryFormViewModelBuilder
                .Simple()
                .Build();

            controller1.Add(viewModel);

            mockRepo.Verify(r => r.AddResponses(It.IsAny<WittyEntry>()));
            mockRepo.Verify(r => r.Add(It.IsAny<WittyEntry>()), Times.Never);
        }
        

        [Test]
        public void WhenIndexActionCalled_ThenShowFormViewModelWithDefaultProperties()
        {
            GetWittyEntryFormViewModel viewModel = UnitTestUtility
                .GetModel<GetWittyEntryFormViewModel>(controller.Index());

            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel, Is.TypeOf<GetWittyEntryFormViewModel>());
            Assert.That(viewModel.Question, Is.Empty);
        }

        [Test]
        public void GivenWittyEntryDoesNotExists_WhenHttpPostGetActionCalled_ThenReturnFormViewModelWithNotExistsIndication()
        {
            WittyEntryController controller1;
            mockRepo
                .Setup(r => r.Exists(It.IsAny<string>()))
                .Returns(false);
            controller1 = new WittyEntryController(mockRepo.Object);

            GetWittyEntryFormViewModel viewModel = UnitTestUtility
                .GetModel<GetWittyEntryFormViewModel>(controller.Get(new GetWittyEntryFormViewModel()));

            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel, Is.TypeOf<GetWittyEntryFormViewModel>());
            Assert.That(viewModel.NotExistsMessage, Is.EqualTo("Question does not exists"));
        }

        [Test]
        public void GivenValidFormViewModel_WhenHttpPostGetActionCalled_ThenCallGetMethodInRepo()
        {
            controller.Get(new GetWittyEntryFormViewModel());

            mockRepo.Verify(r => r.Get(It.IsAny<string>()));
        }

        [Test]
        public void GivenValidFormViewModel_WhenHttpPostGetActionCalled_ThenReturnWittyEntryViewModelObject()
        {
            WittyEntryViewModel viewModel = UnitTestUtility
                .GetModel<WittyEntryViewModel>(controller.Get(new GetWittyEntryFormViewModel()));

            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel, Is.TypeOf<WittyEntryViewModel>());
        }

        private void assertDefaultAddFormViewModelProperties(AddWittyEntryFormViewModel returnedViewModel)
        {
            Assert.That(returnedViewModel, Is.Not.Null);
            Assert.That(returnedViewModel, Is.TypeOf<AddWittyEntryFormViewModel>());
            Assert.That(returnedViewModel.QuestionString, Is.Empty);
            Assert.That(returnedViewModel.GetCategoryTextList(),
                Is.EquivalentTo(expectedCategoryTextList()));
            Assert.That(returnedViewModel.Response, Is.Empty);
        }

        private List<string> expectedCategoryTextList()
        {
            List<string> expected = new List<string>();

            expected.Add(Messenger.ChooseCategory);
            expected.Add(Messenger.Sarcasm);
            expected.Add(Messenger.SelfDeprecation);
            expected.Add(Messenger.Exaggeration);
            expected.Add(Messenger.Shock);
            expected.Add(Messenger.WordPlay);
            expected.Add(Messenger.Association);
            expected.Add(Messenger.Reverse);
            expected.Add(Messenger.Misdirect);
            expected.Add(Messenger.Analogy);

            return expected;
        }
    }
}
