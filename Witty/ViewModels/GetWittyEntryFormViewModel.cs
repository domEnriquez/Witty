using System.ComponentModel.DataAnnotations;

namespace Witty.ViewModels
{
    public class GetWittyEntryFormViewModel
    {
        public GetWittyEntryFormViewModel()
        {
            Question = string.Empty;
        }

        [Display(Name = "Search a Question")]
        public string Question { get; set; }

        public string NotExistsMessage { get; set; }
    }
}
