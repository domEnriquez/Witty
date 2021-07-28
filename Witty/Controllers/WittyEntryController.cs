using Microsoft.AspNetCore.Mvc;
using System;
using Witty.Constants;
using Witty.Mappers;
using Witty.Models;
using Witty.Repositories;
using Witty.ViewModels;

namespace Witty.Controllers
{
    public class WittyEntryController : Controller
    {
        private readonly WittyEntryRepository wittyEntryRepo;

        public WittyEntryController(WittyEntryRepository wittyEntryRepo)
        {
            this.wittyEntryRepo = wittyEntryRepo;
        }

        public IActionResult Add()
        {
            WittyEntryViewModel viewModel = new WittyEntryViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(WittyEntryViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException();

            WittyEntry we = new WittyEntryMapper().Map(viewModel);

            wittyEntryRepo.Add(we);

            viewModel.DefaultProperties();
            viewModel.AddSuccessMessage = Messenger.AddWittyEntrySuccess;

            return View(new WittyEntryViewModel());
        }
    }
}
