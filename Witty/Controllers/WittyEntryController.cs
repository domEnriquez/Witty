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

        public IActionResult Index()
        {
            GetWittyEntryFormViewModel viewModel = new GetWittyEntryFormViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Get(GetWittyEntryFormViewModel formViewModel)
        {
            if (formViewModel == null)
                throw new ArgumentNullException();

            if(wittyEntryRepo.Exists(formViewModel.Question))
            {
                WittyEntry wittyEntry = wittyEntryRepo.GetByQuestion(formViewModel.Question);
                WittyEntryViewModel vm = new WittyEntryMapper().Map(wittyEntry);

                return View(vm);
            }

            formViewModel.NotExistsMessage = "Question does not exists";

            return View("Index", formViewModel);

        }

        public IActionResult Add()
        {
            AddWittyEntryFormViewModel viewModel = new AddWittyEntryFormViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(AddWittyEntryFormViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException();

            if(ModelState.IsValid)
            {
                WittyEntry we = new WittyEntryMapper().Map(viewModel);

                if(wittyEntryRepo.Exists(we.Question))
                    wittyEntryRepo.AddResponses(we);
                else
                    wittyEntryRepo.Add(we);

                wittyEntryRepo.Save();

                viewModel.AddSuccessMessage = Messenger.AddWittyEntrySuccess;
            }

            return View(viewModel);
        }
    }
}
