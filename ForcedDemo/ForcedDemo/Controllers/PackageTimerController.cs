using ForcedDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using ForcedDemo.Models.PackBooking;
using System.Net;

namespace ForcedDemo.Controllers
{
    public class PackageTimerController : Controller
    {
        // GET: PackageTimer
        private ApplicationDbContext db = new ApplicationDbContext();

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
    }
}