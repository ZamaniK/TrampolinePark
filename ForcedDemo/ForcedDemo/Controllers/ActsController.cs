using ForcedDemo.Services;
using ForcedDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForcedDemo.Controllers
{
    public class ActsController : Controller
    {
        // GET: Acts
        public ActionResult Index()
        {
            ActViewModel model = new ActViewModel();
            ActivityTypeService service = new ActivityTypeService();
            ActivityService activityServiceService = new ActivityService();

            model.ActivityTypes = service.GetAllActivityTypes();
            model.Activities = activityServiceService.GetAllActivities();
            return View(model);
        }
    }
}