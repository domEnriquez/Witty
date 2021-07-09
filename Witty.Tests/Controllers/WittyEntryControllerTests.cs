using NUnit.Framework;
using System.Collections.Generic;
using Witty.Constants;
using Witty.Controllers;
using Witty.Tests.Utility;
using Witty.ViewModels;

namespace Witty.Tests.Controllers
{
    [TestFixture]
    public class WittyEntryControllerTests
    {
        WittyEntryController controller;

        [SetUp]
        public void SetUp()
        {
            controller = new WittyEntryController();
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
            
            WittyEntryViewModel viewModel = UnitTestUtility
                .GetModel<WittyEntryViewModel>(controller.Add());

            Assert.That(viewModel.GetResponseCategoriesTexts(), Is.EquivalentTo(expected));
        }

        [Test]
        public void whenAddPageCalled_ThenResponseFieldShouldBeEmpty()
        {
            WittyEntryViewModel viewModel = UnitTestUtility
                .GetModel<WittyEntryViewModel>(controller.Add());

            Assert.That(viewModel.Response, Is.Empty);
        }
    }
}
