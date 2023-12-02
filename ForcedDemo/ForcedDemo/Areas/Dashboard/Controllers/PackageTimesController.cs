using ForcedDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Net;
using ForcedDemo.Models.PackBooking;

namespace ForcedDemo.Areas.Dashboard.Controllers
{
    public class PackageTimesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityTimes
        public ActionResult Index()
        {
            var activityTimes1 = db.PackageTimes1.Include(a => a.AccomodationPackages).Include(a => a.PackageTimes);
            return View(activityTimes1.ToList());
        }
        public ActionResult PackageBooking(int id)
        {
            var activityTimes1 = db.PackageTimes1.Include(a => a.AccomodationPackages).Include(a => a.PackageTimes);
            return View(activityTimes1.ToList().Where(x => x.AccomodationPackageId == id));
        }
        // GET: ActivityTimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTime packageTime = db.PackageTimes1.Find(id);
            if (packageTime == null)
            {
                return HttpNotFound();
            }
            return View(packageTime);
        }

        // GET: ActivityTimes/Create
        public ActionResult Create()
        {
            ViewBag.AccomodationPackageId = new SelectList(db.AccomodationPackages, "ID", "Name");
            ViewBag.PackageTimesId = new SelectList(db.PackageTimes, "PackageTimesId", "SlotTime");
            return View();
        }

        // POST: ActivityTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PackageTimeId,AccomodationPackageId,PackageTimesId")] PackageTime packageTime)
        {
            if (ModelState.IsValid)
            {
                db.PackageTimes1.Add(packageTime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccomodationPackageId = new SelectList(db.AccomodationPackages, "ID", "Name", packageTime.AccomodationPackageId);
            ViewBag.PackageTimesId = new SelectList(db.PackageTimes, "PackageTimesId", "SlotTime", packageTime.PackageTimesId);
            return View(packageTime);
        }

        // GET: ActivityTimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTime packageTime = db.PackageTimes1.Find(id);
            if (packageTime == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivitiesId = new SelectList(db.AccomodationPackages, "AccomodationPackagesId", "Name", packageTime.AccomodationPackageId);
            ViewBag.ActivityTimesId = new SelectList(db.PackageTimes, "PackageTimesId", "SlotTime", packageTime.PackageTimesId);
            return View(packageTime);
        }

        // POST: ActivityTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityTimeId,ActivitiesId,ActivityTimesId")] PackageTime packageTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(packageTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivitiesId = new SelectList(db.AccomodationPackages, "AccomodationPackagesId", "ActivityName", packageTime.AccomodationPackageId);
            ViewBag.ActivityTimesId = new SelectList(db.PackageTimes, "PackageTimesId", "SlotTime", packageTime.PackageTimesId);
            return View(packageTime);
        }

        // GET: ActivityTimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTime packageTime = db.PackageTimes1.Find(id);
            if (packageTime == null)
            {
                return HttpNotFound();
            }
            return View(packageTime);
        }

        // POST: ActivityTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PackageTime packageTime = db.PackageTimes1.Find(id);
            db.PackageTimes1.Remove(packageTime);
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