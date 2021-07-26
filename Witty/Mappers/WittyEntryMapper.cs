using System;
using Witty.Models;
using Witty.ViewModels;

namespace Witty.Mappers
{
    public class WittyEntryMapper
    {
        public WittyEntry Map(WittyEntryViewModel viewModel)
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
    }
}
