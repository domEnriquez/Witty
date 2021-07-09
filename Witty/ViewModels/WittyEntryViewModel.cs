using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Witty.Constants;
using Witty.Extensions;

namespace Witty.ViewModels
{
    public class WittyEntryViewModel
    {
        public WittyEntryViewModel()
        {
            QuestionString = string.Empty;
            Response = string.Empty;
            populateResponseCategories();
        }

        public string QuestionString { get; set; }
        public List<SelectListItem> ResponseCategories { get; set; }
        public string Response { get; set; }

        private void populateResponseCategories()
        {
            ResponseCategories = new List<SelectListItem>();

            ResponseCategories.AddUnselected(Messenger.ChooseCategory);
            ResponseCategories.AddUnselected(Messenger.Sarcasm);
            ResponseCategories.AddUnselected(Messenger.SelfDeprecation);
            ResponseCategories.AddUnselected(Messenger.Exaggeration);
            ResponseCategories.AddUnselected(Messenger.Shock);
            ResponseCategories.AddUnselected(Messenger.WordPlay);
            ResponseCategories.AddUnselected(Messenger.Association);
            ResponseCategories.AddUnselected(Messenger.Reverse);
            ResponseCategories.AddUnselected(Messenger.Misdirect);
            ResponseCategories.AddUnselected(Messenger.Analogy);
        }

        public List<string> GetResponseCategoriesTexts() 
        {
            return ResponseCategories.Select(x => x.Text).ToList();
        } 
    }
}
