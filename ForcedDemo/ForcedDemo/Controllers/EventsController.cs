using ForcedDemo.Services;
using ForcedDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForcedDemo.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            EventViewModel model = new EventViewModel();
            AccomodationTypeService service = new AccomodationTypeService();
            AccomodationPackageService accomodationPackageService = new AccomodationPackageService();

            model.AccomodationTypes = service.GetAllAccomodationTypes();
            model.AccomodationPackages = accomodationPackageService.GetAllAccomodationPackages();
            return View(model);
        }
    }
}