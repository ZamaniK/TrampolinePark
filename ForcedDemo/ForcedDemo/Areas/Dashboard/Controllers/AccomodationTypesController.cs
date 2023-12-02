using ForcedDemo.Areas.Dashboard.ViewModels;
using ForcedDemo.Entities;
using ForcedDemo.Services;

using System.Web.Mvc;

namespace ForcedDemo.Areas.Dashboard.Controllers
{
    public class AccomodationTypesController : Controller
    {
        AccomodationTypeService accomodationTypeService = new AccomodationTypeService();
        // GET: Dashboard/AccomodationTypes
        public ActionResult Index(string searchTerm)
        {
            AccomodationTypeListingModel model = new AccomodationTypeListingModel();

            model.SearchTerm = searchTerm;

            model.AccomodationTypes = accomodationTypeService.SearchAccomodationTypes(searchTerm);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();

            if (ID.HasValue)  //we are trying to edit a record
            {
                var accomodationType = accomodationTypeService.GetAccomodationTypeByID(ID.Value);

                model.ID = accomodationType.ID;
                model.Name = accomodationType.Name;
                model.Description = accomodationType.Description;
            }
            return PartialView("_Action", model);
        }


        [HttpPost]
        public JsonResult Action(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0)
            {
                var accomodationType = accomodationTypeService.GetAccomodationTypeByID(model.ID);

                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;

                result = accomodationTypeService.UpdateAccomodationType(accomodationType);
            }
            else
            {
                AccomodationType accomodationType = new AccomodationType();

                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;

                result = accomodationTypeService.SaveAccomodationType(accomodationType);
            }


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Types." };
            }
            return json;
        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();

            var accomodationType = accomodationTypeService.GetAccomodationTypeByID(ID);

            model.ID = accomodationType.ID;


            return PartialView("_Delete", model);
        }


        [HttpPost]
        public JsonResult Delete(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var accomodationType = accomodationTypeService.GetAccomodationTypeByID(model.ID);



            result = accomodationTypeService.DeleteAccomodationType(accomodationType);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Types." };
            }
            return json;
        }


    }
}