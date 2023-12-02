using ForcedDemo.Services;
using ForcedDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForcedDemo.Controllers
{
    public class AccomodationsController : Controller
    {
        AccomodationTypeService accomodationTypeService = new AccomodationTypeService();
        AccomodationPackageService accomodationPackageService = new AccomodationPackageService();
        AccomodationService accomodationService = new AccomodationService();

        //// GET: Accomodations
        public ActionResult Index(int accomodationTypeID, int? accomodationPackageID)
        {
            AccomodationsViewModel model = new AccomodationsViewModel();

            model.AccomodationType = accomodationTypeService.GetAccomodationTypeByID(accomodationTypeID);
            model.AccomodationPackages = accomodationPackageService.GetAllAccomodationPackagesByAccomodationType(accomodationTypeID);

            model.SelectedAccompdationPackageID = accomodationPackageID.HasValue ? accomodationPackageID.Value : model.AccomodationPackages.First().ID;
            model.Accomodations = accomodationService.GetAllAccomodationsByAccomodationPackage(model.SelectedAccompdationPackageID);
            return View(model);
        }

        public ActionResult Details(int accomodationPackageID)
        {
            AccomodationPackageDetailsViewModel model = new AccomodationPackageDetailsViewModel();

            model.AccomodationPackage = accomodationPackageService.GetAccomodationPackageByID(accomodationPackageID);
            return View(model);
        }

        public ActionResult CheckAvailability(CheckAccomodationAvailabilityViewModel model)
        {
            return View(model);
        }
    }
}