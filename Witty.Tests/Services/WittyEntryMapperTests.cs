using NUnit.Framework;
using Witty.Constants;
using Witty.Mappers;
using Witty.Models;
using Witty.Tests.Builders;
using Witty.ViewModels;

namespace Witty.Tests.Services
{
    [TestFixture]
    public class WittyEntryMapperTests
    {
        private WittyEntryMapper mapper;

        [SetUp]
        public void Setup()
        {
            mapper = new WittyEntryMapper();
        }

        [Test]
        public void adapt_AddWittyEntryFormViewModel_WittyEntry()
        {
            AddWittyEntryFormViewModel formViewModel = AddWittyEntryFormViewModelBuilder
                .Default()
                .WithQuestionString("What?")
                .WithResponseCategory(Messenger.Analogy)
                .WithResponse("Yes")
                .Build();

            WittyEntry expected = WittyEntryBuilder
                .Default()
                .WithQuestion("What?")
                .WithResponses(ResponseListBuilder.Default()
                    .WithResponseCategories(Messenger.Analogy)
                    .WithResponseStrings("Yes").Build())
                .Build();

            AreEqual(mapper.Map(formViewModel), expected);
        }

        public void adapt_WittyEntry_To_WittyEntryViewModel()
        {
            WittyEntry wittyEntry = WittyEntryBuilder
                .Default()
                .WithQuestion("What?")
                .WithResponses(ResponseListBuilder.Default()
                    .WithResponseCategories(Messenger.Analogy)
                    .WithResponseStrings("Yes").Build())
                .Build();

            WittyEntryViewModel expected = WittyEntryViewModelBuilder
                .Default()
                .WithQuestion("What?")
                .WithResponses(ResponseListBuilder.Default()
                    .WithResponseCategories(Messenger.Analogy)
                    .WithResponseStrings("Yes").Build())
                .Build();

            AreEqual(mapper.Map(wittyEntry), expected);
        }

        public void AreEqual(WittyEntry actual, WittyEntry expected)
        {
            Assert.That(actual.Question, Is.EqualTo(expected.Question));
            Assert.That(actual.Responses, Is.EquivalentTo(expected.Responses));
        }

        public void AreEqual(WittyEntryViewModel actual, WittyEntryViewModel expected)
        {
            Assert.That(actual.Question, Is.EqualTo(expected.Question));
            Assert.That(actual.Responses, Is.EquivalentTo(expected.Responses));
        }
    }
}
