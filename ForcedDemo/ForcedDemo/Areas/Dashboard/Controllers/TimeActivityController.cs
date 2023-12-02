using ForcedDemo.Models;
using ForcedDemo.Models.ActBooking;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ForcedDemo.Areas.Dashboard.Controllers
{
    public class TimeActivityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeActivity
        public ActionResult Index()
        {
            return View(db.ActivityTimes.ToList());
        }

        // GET: TimeActivity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityTimes activityTimes = db.ActivityTimes.Find(id);
            if (activityTimes == null)
            {
                return HttpNotFound();
            }
            return View(activityTimes);
        }

        // GET: TimeActivity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TimeActivity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityTimesId,SlotTime")] ActivityTimes activityTimes)
        {
            if (ModelState.IsValid)
            {
                db.ActivityTimes.Add(activityTimes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activityTimes);
        }

        // GET: TimeActivity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityTimes activityTimes = db.ActivityTimes.Find(id);
            if (activityTimes == null)
            {
                return HttpNotFound();
            }
            return View(activityTimes);
        }

        // POST: TimeActivity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityTimesId,SlotTime")] ActivityTimes activityTimes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityTimes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activityTimes);
        }

        // GET: TimeActivity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityTimes activityTimes = db.ActivityTimes.Find(id);
            if (activityTimes == null)
            {
                return HttpNotFound();
            }
            return View(activityTimes);
        }

        // POST: TimeActivity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityTimes activityTimes = db.ActivityTimes.Find(id);
            db.ActivityTimes.Remove(activityTimes);
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