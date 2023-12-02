using ForcedDemo.Areas.Dashboard.ViewModels;
using ForcedDemo.Entities;
using ForcedDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForcedDemo.Areas.Dashboard.Controllers
{
    public class ActivityTypesController : Controller
    {
        ActivityTypeService activityTypeService = new ActivityTypeService();
        // GET: Dashboard/AccomodationTypes
        public ActionResult Index(string searchTerm)
        {
            ActivityTypeListingModel model = new ActivityTypeListingModel();

            model.SearchTerm = searchTerm;

            model.ActivityTypes = activityTypeService.SearchActivityTypes(searchTerm);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            ActivityTypeActionModel model = new ActivityTypeActionModel();

            if (ID.HasValue)  //we are trying to edit a record
            {
                var activity = activityTypeService.GetActivityTypeByID(ID.Value);

                model.ID = activity.ID;
                model.Name = activity.Name;
                model.Description = activity.Description;
            }
            return PartialView("_Action", model);
        }


        [HttpPost]
        public JsonResult Action(ActivityTypeActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0)
            {
                var activitytype = activityTypeService.GetActivityTypeByID(model.ID);

                activitytype.Name = model.Name;
                activitytype.Description = model.Description;

                result = activityTypeService.UpdateActivityType(activitytype);
            }
            else
            {
                ActivityType accomodationType = new ActivityType();

                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;

                result = activityTypeService.SaveActivityType(accomodationType);
            }


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Activity Types." };
            }
            return json;
        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            ActivityTypeActionModel model = new ActivityTypeActionModel();

            var activityType = activityTypeService.GetActivityTypeByID(ID);

            model.ID = activityType.ID;


            return PartialView("_Delete", model);
        }


        [HttpPost]
        public JsonResult Delete(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var activityType = activityTypeService.GetActivityTypeByID(model.ID);



            result = activityTypeService.DeleteActivityType(activityType);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Activity Types." };
            }
            return json;
        }


    }
}