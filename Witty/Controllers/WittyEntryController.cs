using Microsoft.AspNetCore.Mvc;
using Witty.ViewModels;

namespace Witty.Controllers
{
    public class WittyEntryController : Controller
    {
        public IActionResult Add()
        {
            WittyEntryViewModel model = new WittyEntryViewModel();


            return View(model);
        }
    }
}
