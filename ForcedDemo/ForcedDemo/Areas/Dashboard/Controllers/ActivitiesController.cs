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
    public class ActivitiesController : Controller
    {
        ActivityService activityService = new ActivityService();
        ActivityTypeService activityTypeService = new ActivityTypeService();
        DashboardService dashboardService = new DashboardService();
        // GET: Dashboard/AccomodationTypes
        public ActionResult Index(string searchTerm, int? activityTypeID, int? page)
        {
            ActivityListingModel model = new ActivityListingModel();

            int recordSize = 5;
            page = page ?? 1;

            model.SearchTerm = searchTerm;
            model.ActivityTypeID = activityTypeID;

            model.Activities = activityService.SearchActivity(searchTerm, activityTypeID, page.Value, recordSize);

            model.ActivityTypes = activityTypeService.GetAllActivityTypes();

            var totalRecords = activityService.SearchActivityCount(searchTerm, activityTypeID);
            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            ActivityActionModel model = new ActivityActionModel();

            if (ID.HasValue)  //we are trying to edit a record
            {
                var activity = activityService.GetActivityByID(ID.Value);

                model.ID = activity.ActivitiesId;
                model.ActivityTypeID = activity.ActivityTypeID;
                model.Name = activity.ActivityName;
                model.Description = activity.Description;
                model.NoOfPeople = activity.Capacity;
                model.FeePerDay = activity.ActivityPrice;
                model.ActivityPictures = activityService.GetPicturesByActivityID(activity.ActivitiesId);
            }
            model.ActivityTypes = activityTypeService.GetAllActivityTypes();
            return PartialView("_Action", model);
        }


        [HttpPost]
        public JsonResult Action(ActivityActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;
            List<int> pictureIDs = !string.IsNullOrEmpty(model.PictureIDs) ? model.PictureIDs.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>();
            var pictures = dashboardService.GetPicturesByID(pictureIDs);

            if (model.ID > 0)
            {
                var activity = activityService.GetActivityByID(model.ID);

                activity.ActivityTypeID = model.ActivityTypeID;
                activity.ActivityName = model.Name;
                activity.Description = model.Description;
                activity.Capacity = model.NoOfPeople;
                activity.ActivityPrice = model.FeePerDay;

                activity.ActivityPictures.Clear();
                activity.ActivityPictures.AddRange(pictures.Select(x => new ActivityPictures() { ActivitiesId = activity.ActivitiesId, PictureID = x.ID }));

                result = activityService.UpdateActivity(activity);
            }
            else
            {
                Activvity activity = new Activvity();

                activity.ActivityTypeID = model.ActivityTypeID;
                activity.ActivityName = model.Name;
                activity.Description = model.Description;
                activity.Capacity = model.NoOfPeople;
                activity.ActivityPrice = model.FeePerDay;

                activity.ActivityPictures = new List<ActivityPictures>();
                activity.ActivityPictures.AddRange(pictures.Select(x => new ActivityPictures() { PictureID = x.ID }));

                result = activityService.SaveActivity(activity);
            }


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Activity." };
            }
            return json;
        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            ActivityActionModel model = new ActivityActionModel();

            var activity = activityService.GetActivityByID(ID);

            model.ID = activity.ActivitiesId;


            return PartialView("_Delete", model);
        }


        [HttpPost]
        public JsonResult Delete(ActivityActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var activity = activityService.GetActivityByID(model.ID);



            result = activityService.DeleteActivity(activity);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Packages." };
            }
            return json;
        }
    }
}