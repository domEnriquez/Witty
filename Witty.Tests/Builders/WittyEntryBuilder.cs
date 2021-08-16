using System.Collections.Generic;
using Witty.Constants;
using Witty.Models;

namespace Witty.Tests.Builders
{
    public class WittyEntryBuilder
    {
        private readonly WittyEntry wittyEntry = new WittyEntry();

        public static WittyEntryBuilder Default()
        {
            return new WittyEntryBuilder();
        }

        public WittyEntryBuilder Simple()
        {
            wittyEntry.Id = 0;
            wittyEntry.Question = "What?";
            wittyEntry.Responses = ResponseListBuilder
                .Default()
                .WithResponseCategories(Messenger.Analogy)
                .WithResponseStrings("Yes").Build();

            return this;
        }

        public WittyEntryBuilder WithId(int id)
        {
            wittyEntry.Id = id;

            return this;
        }

        public WittyEntryBuilder WithQuestion(string question)
        {
            wittyEntry.Question = question;

            return this;
        }

        public WittyEntryBuilder WithResponses(List<Response> responses)
        {
            wittyEntry.Responses = responses;

            return this;
        }

        public WittyEntry Build()
        {
            return wittyEntry;
        }
    }
}
