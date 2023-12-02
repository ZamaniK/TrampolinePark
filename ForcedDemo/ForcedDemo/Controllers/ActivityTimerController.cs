using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using ForcedDemo.Models;

namespace ForcedDemo.Controllers
{
    public class ActivityTimerController : Controller
    {
        // GET: ActivityTimer
        public ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var activityTimes1 = db.ActivityTimes1.Include(a => a.Activities).Include(a => a.ActivityTimes);
            return View(activityTimes1.ToList());
        }
        public ActionResult ActivityBooking(int id)
        {
            var activityTimes1 = db.ActivityTimes1.Include(a => a.Activities).Include(a => a.ActivityTimes);
            return View(activityTimes1.ToList().Where(x => x.ActivitiesId == id));
        }
    }
}