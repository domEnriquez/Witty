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

        public WittyEntryViewModelBuilder WithQuestion(string question)
        {
            viewModel.Question = question;

            return this;
        }

        public WittyEntryViewModelBuilder WithResponses(List<Response> responses)
        {
            viewModel.Responses = responses;

            return this;
        }

        public WittyEntryViewModel Build()
        {
            return viewModel;
        }
    }
}
