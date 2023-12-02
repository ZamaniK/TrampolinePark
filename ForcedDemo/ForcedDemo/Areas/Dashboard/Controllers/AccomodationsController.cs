using ForcedDemo.Areas.Dashboard.ViewModels;
using ForcedDemo.Entities;
using ForcedDemo.Services;
using ForcedDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForcedDemo.Areas.Dashboard.Controllers
{
    public class AccomodationsController : Controller
    {
        AccomodationService accomodationService = new AccomodationService();
        AccomodationPackageService accomodationPackageService = new AccomodationPackageService();
        // GET: Dashboard/AccomodationTypes
        public ActionResult Index(string searchTerm, int? accomodationPackageID, int? page)
        {
            AccomodationListingModel model = new AccomodationListingModel();

            int recordSize = 5;
            page = page ?? 1;

            model.SearchTerm = searchTerm;
            model.AccomodationPackageID = accomodationPackageID;

            model.Accomodations = accomodationService.SearchAccomodations(searchTerm, accomodationPackageID, page.Value, recordSize);

            model.AccomodationPackages = accomodationPackageService.GetAllAccomodationPackages();

            var totalRecords = accomodationService.SearchAccomodationsCount(searchTerm, accomodationPackageID);
            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationActionModel model = new AccomodationActionModel();

            if (ID.HasValue)  //we are trying to edit a record
            {
                var accomodation = accomodationService.GetAccomodationByID(ID.Value);

                model.ID = accomodation.ID;
                model.AccomodationPackageID = accomodation.AccomodationPackageID;
                model.Name = accomodation.Name;
                model.Address = accomodation.Address;
            }
            model.AccomodationPackages = accomodationPackageService.GetAllAccomodationPackages();
            return PartialView("_Action", model);
        }


        [HttpPost]
        public JsonResult Action(AccomodationActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0)
            {
                var accomodation = accomodationService.GetAccomodationByID(model.ID);

                accomodation.AccomodationPackageID = model.AccomodationPackageID;
                accomodation.Name = model.Name;
                accomodation.Address = model.Address;

                result = accomodationService.UpdateAccomodation(accomodation);
            }
            else
            {
                Accomodation accomodation = new Accomodation();

                accomodation.AccomodationPackageID = model.AccomodationPackageID;
                accomodation.Name = model.Name;
                accomodation.Address = model.Address;

                result = accomodationService.SaveAccomodation(accomodation);
            }


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation." };
            }
            return json;
        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationActionModel model = new AccomodationActionModel();

            var accomodation = accomodationService.GetAccomodationByID(ID);

            model.ID = accomodation.ID;


            return PartialView("_Delete", model);
        }


        [HttpPost]
        public JsonResult Delete(AccomodationActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var accomodation = accomodationService.GetAccomodationByID(model.ID);



            result = accomodationService.DeleteAccomodation(accomodation);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodatio." };
            }
            return json;
        }
    }
}