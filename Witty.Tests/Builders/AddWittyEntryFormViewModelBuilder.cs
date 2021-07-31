using Witty.Constants;
using Witty.ViewModels;

namespace Witty.Tests.Builders
{

    public class AddWittyEntryFormViewModelBuilder
    {
        private readonly AddWittyEntryFormViewModel viewModel = new AddWittyEntryFormViewModel();

        public static AddWittyEntryFormViewModelBuilder Default()
        {
            return new AddWittyEntryFormViewModelBuilder();
        }

        public static AddWittyEntryFormViewModelBuilder Simple()
        {
            return Default()
                .WithQuestionString("What?")
                .WithResponseCategory(Messenger.Analogy)
                .WithResponse("Yes");
        }


        public AddWittyEntryFormViewModelBuilder WithQuestionString(string questionString)
        {
            viewModel.QuestionString = questionString;

            return this;
        }

        public AddWittyEntryFormViewModelBuilder WithResponseCategory(string responseCategory)
        {
            viewModel.ResponseCategory = responseCategory;

            return this;
        }

        public AddWittyEntryFormViewModelBuilder WithResponse(string response)
        {
            viewModel.Response = response;

            return this;
        }

        public AddWittyEntryFormViewModel Build()
        {
            return viewModel;
        }
    }
}
