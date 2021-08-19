using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Witty.Constants;
using Witty.Extensions;

namespace Witty.ViewModels
{
    public class AddWittyEntryFormViewModel
    {
        public AddWittyEntryFormViewModel()
        {
            QuestionString = string.Empty;
            Response = string.Empty;
            AddSuccessMessage = string.Empty;
            populateResponseCategories();
        } 

        [Display(Name ="Question")]
        [Required(ErrorMessage = "Please enter a question")]
        public string QuestionString { get; set; }

        public List<SelectListItem> ResponseCategories { get; set; }

        [Display(Name = "Response Category")]
        [Required]
        [ValidResponseCategory]
        public string ResponseCategory { get; set; }

        [Required]
        public string Response { get; set; }

        public string AddSuccessMessage { get; set; }

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

        public List<string> GetCategoryTextList() 
        {
            return ResponseCategories.Select(x => x.Text).ToList();
        }
    }
}
