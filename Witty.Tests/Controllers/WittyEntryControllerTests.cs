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

            controller = new WittyEntryController(mockRepo.Object);
        }

        [Test]
        public void WhenAddHttpGetActionCalled_ThenShowDefaultViewModelProperties()
        {
            AddWittyEntryFormViewModel viewModel = UnitTestUtility
                .GetModel<AddWittyEntryFormViewModel>(controller.Add());

            assertDefaultAddFormViewModelProperties(viewModel);
        }

        [Test]
        public void GivenNullWittyEntryViewModel_WhenAddActionCalled_ThenThrowArgumentNullException()
        {
            Assert.That(() => controller.Add(null), 
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GivenAValidWittyEntryViewModel_WhenAddActionCalled_ThenCallAddMethodInRepo()
        {
            AddWittyEntryFormViewModel viewModel = AddWittyEntryFormViewModelBuilder
                .Simple()
                .Build();

            controller.Add(viewModel);

            mockRepo.Verify(r => r.Add(It.IsAny<WittyEntry>()));
        }

        [Test]
        public void GivenAValidWittyEntryViewModel_WhenAddActionCalled_ThenReturnDefaultViewWithSuccessIndication()
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
        public void WhenIndexActionCalled_ThenShowGetWittyEntryFormViewModelWithDefaultProperties()
        {
            GetWittyEntryFormViewModel viewModel = UnitTestUtility
                .GetModel<GetWittyEntryFormViewModel>(controller.Index());

            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel, Is.TypeOf<GetWittyEntryFormViewModel>());
            Assert.That(viewModel.Question, Is.Empty);
        }

        [Test]
        public void GivenAValidGetWittyEntryFormViewModel_WhenGetActionCalled_ThenCallGetWittyEntryMethodInRepo()
        {
            controller.Get(new GetWittyEntryFormViewModel());

            mockRepo.Verify(r => r.Get(It.IsAny<string>()));
        }

        [Test]
        public void GivenAValidGetWittyEntryFormViewModel_WhenGetActionCalled_ThenReturnWittyEntryViewModelObject()
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
