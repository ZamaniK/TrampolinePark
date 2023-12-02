using ForcedDemo.Models;
using ForcedDemo.Models.ActBooking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace ForcedDemo.Areas.Dashboard.Controllers
{
    public class ActivityTimesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityTimes
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
        // GET: ActivityTimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityTime activityTime = db.ActivityTimes1.Find(id);
            if (activityTime == null)
            {
                return HttpNotFound();
            }
            return View(activityTime);
        }

        // GET: ActivityTimes/Create
        public ActionResult Create()
        {
            ViewBag.ActivitiesId = new SelectList(db.Activities, "ActivitiesId", "ActivityName");
            ViewBag.ActivityTimesId = new SelectList(db.ActivityTimes, "ActivityTimesId", "SlotTime");
            return View();
        }

        // POST: ActivityTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityTimeId,ActivitiesId,ActivityTimesId")] ActivityTime activityTime)
        {
            if (ModelState.IsValid)
            {
                db.ActivityTimes1.Add(activityTime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivitiesId = new SelectList(db.Activities, "ActivitiesId", "ActivityName", activityTime.ActivitiesId);
            ViewBag.ActivityTimerId = new SelectList(db.ActivityTimes, "ActivityTimesId", "SlotTime", activityTime.ActivityTimesId);
            return View(activityTime);
        }

        // GET: ActivityTimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityTime activityTime = db.ActivityTimes1.Find(id);
            if (activityTime == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivitiesId = new SelectList(db.Activities, "ActivitiesId", "ActivityName", activityTime.ActivitiesId);
            ViewBag.ActivityTimesId = new SelectList(db.ActivityTimes, "ActivityTimesId", "SlotTime", activityTime.ActivityTimesId);
            return View(activityTime);
        }

        // POST: ActivityTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityTimeId,ActivitiesId,ActivityTimesId")] ActivityTime activityTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivitiesId = new SelectList(db.Activities, "ActivitiesId", "ActivityName", activityTime.ActivitiesId);
            ViewBag.ActivityTimesId = new SelectList(db.ActivityTimes, "ActivityTimesId", "SlotTime", activityTime.ActivityTimesId);
            return View(activityTime);
        }

        // GET: ActivityTimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityTime activityTime = db.ActivityTimes1.Find(id);
            if (activityTime == null)
            {
                return HttpNotFound();
            }
            return View(activityTime);
        }

        // POST: ActivityTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityTime activityTime = db.ActivityTimes1.Find(id);
            db.ActivityTimes1.Remove(activityTime);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}