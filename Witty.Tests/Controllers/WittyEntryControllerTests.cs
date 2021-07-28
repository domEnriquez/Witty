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
        WittyEntryRepository wittyEntryRepo;
        Mock<WittyEntryRepository> mockRepo;

        [SetUp]
        public void SetUp()
        {
            mockRepo = new Mock<WittyEntryRepository>();
            controller = new WittyEntryController(mockRepo.Object);
        }

        [Test]
        public void whenAddPageCalled_ThenWittyEntryViewModelIsReturned()
        {
            WittyEntryViewModel viewModel = UnitTestUtility
                .GetModel<WittyEntryViewModel>(controller.Add());

            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel, Is.TypeOf<WittyEntryViewModel>());
        }
        
        [Test]
        public void whenAddPageCalled_ThenQuestionFieldShouldBeEmpty()
        {
            WittyEntryViewModel viewModel = UnitTestUtility
                .GetModel<WittyEntryViewModel>(controller.Add());

            Assert.That(viewModel.QuestionString, Is.Empty);
        }

        [Test]
        public void whenAddPageCalled_ThenResponseCategoryIsPopulated()
        {

            WittyEntryViewModel viewModel = UnitTestUtility
                .GetModel<WittyEntryViewModel>(controller.Add());

            Assert.That(viewModel.GetCategoryTextList(), 
                Is.EquivalentTo(expectedCategoryTextList()));
        }

        [Test]
        public void whenAddPageCalled_ThenResponseFieldShouldBeEmpty()
        {
            WittyEntryViewModel viewModel = UnitTestUtility
                .GetModel<WittyEntryViewModel>(controller.Add());

            Assert.That(viewModel.Response, Is.Empty);
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
            WittyEntryViewModel viewModel = WittyEntryViewModelBuilder
                .Simple()
                .Build();

            controller.Add(viewModel);

            mockRepo.Verify(r => r.Add(It.IsAny<WittyEntry>()));
        }

        [Test]
        public void GivenAValidWittyEntryViewModel_WhenAddActionCalled_ThenReturnDefaultViewWithSuccessIndication()
        {
            WittyEntryViewModel viewModel = WittyEntryViewModelBuilder
                .Simple()
                .Build();

            WittyEntryViewModel returnedViewModel = UnitTestUtility
                .GetModel<WittyEntryViewModel>(controller.Add(viewModel));


            Assert.That(returnedViewModel, Is.Not.Null);
            Assert.That(returnedViewModel, Is.TypeOf<WittyEntryViewModel>());
            Assert.That(returnedViewModel.QuestionString, Is.Empty);
            Assert.That(returnedViewModel.GetCategoryTextList(),
                Is.EquivalentTo(expectedCategoryTextList()));
            Assert.That(viewModel.Response, Is.Empty);
            Assert.That(viewModel.AddSuccessMessage, Is.EqualTo(Messenger.AddWittyEntrySuccess));
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
