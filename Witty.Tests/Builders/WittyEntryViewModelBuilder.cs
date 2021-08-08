using System.Collections.Generic;
using Witty.Models;
using Witty.ViewModels;

namespace Witty.Tests.Builders
{
    public class WittyEntryViewModelBuilder
    {
        private readonly WittyEntryViewModel viewModel = new WittyEntryViewModel();

        public static WittyEntryViewModelBuilder Default()
        {
            return new WittyEntryViewModelBuilder();
        }

        internal WittyEntryViewModelBuilder WithId(int id)
        {
            viewModel.WittyEntry.Id = id;

            return this;
        }

        public WittyEntryViewModelBuilder WithQuestion(string question)
        {
            viewModel.WittyEntry.Question = question;

            return this;
        }

        public WittyEntryViewModelBuilder WithResponses(List<Response> responses)
        {
            viewModel.WittyEntry.Responses = responses;

            return this;
        }

        public WittyEntryViewModel Build()
        {
            return viewModel;
        }
    }
}
