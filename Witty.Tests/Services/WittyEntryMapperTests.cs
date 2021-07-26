using NUnit.Framework;
using System;
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
        public void GivenWittyEntryViewModelIsNull_WhenMap_ThenThrowArgumentNullException()
        {
            Assert.That(() => mapper.Map(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void adaptWittyEntryViewModelToWittyEntry()
        {
            WittyEntryViewModel wittyEntryVm = WittyEntryViewModelBuilder
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

            AreEqual(mapper.Map(wittyEntryVm), expected);
        }

        public void AreEqual(WittyEntry actual, WittyEntry expected)
        {
            Assert.That(actual.Question, Is.EqualTo(expected.Question));
            Assert.That(actual.Responses, Is.EquivalentTo(expected.Responses));
        }
    }
}
