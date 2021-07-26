using Witty.Constants;
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

        public static WittyEntryViewModelBuilder Simple()
        {
            return Default()
                .WithQuestionString("What?")
                .WithResponseCategory(Messenger.Analogy)
                .WithResponse("Yes");
        }


        public WittyEntryViewModelBuilder WithQuestionString(string questionString)
        {
            viewModel.QuestionString = questionString;

            return this;
        }

        public WittyEntryViewModelBuilder WithResponseCategory(string responseCategory)
        {
            viewModel.ResponseCategory = responseCategory;

            return this;
        }

        public WittyEntryViewModelBuilder WithResponse(string response)
        {
            viewModel.Response = response;

            return this;
        }

        public WittyEntryViewModel Build()
        {
            return viewModel;
        }
    }
}
