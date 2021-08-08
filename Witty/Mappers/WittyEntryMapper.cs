using System;
using Witty.Models;
using Witty.ViewModels;

namespace Witty.Mappers
{
    public class WittyEntryMapper
    {
        public WittyEntry Map(AddWittyEntryFormViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException();

            WittyEntry wittyEntry = new WittyEntry();

            wittyEntry.Question = viewModel.QuestionString;

            Response response = new Response
            {
                ResponseCategory = viewModel.ResponseCategory,
                ResponseString = viewModel.Response
            };

            wittyEntry.Responses.Add(response);

            return wittyEntry;
        }

        public WittyEntryViewModel Map(WittyEntry wittyEntry)
        {
            if (wittyEntry == null)
                throw new ArgumentNullException();

            WittyEntryViewModel viewModel = new WittyEntryViewModel();

            viewModel.WittyEntry.Id = wittyEntry.Id;
            viewModel.WittyEntry.Question = wittyEntry.Question;
            viewModel.WittyEntry.Responses = wittyEntry.Responses;

            return viewModel;
        }
    }
}
