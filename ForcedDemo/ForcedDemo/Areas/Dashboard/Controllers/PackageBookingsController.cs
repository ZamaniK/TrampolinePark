using ForcedDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using ForcedDemo.Models.PackBooking;
using Microsoft.AspNet.Identity;
using System.Net;

namespace ForcedDemo.Areas.Dashboard.Controllers
{
    public class PackageBookingsController : Controller
    {
        // GET: Dashboard/PackageBookings
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityBookings
        public ActionResult Index()
        {
            var activityBookings = db.PackageBookings.Include(a => a.PackageTime);
            return View(activityBookings.ToList());
        }
        public ActionResult Index2()
        {
            var userName = User.Identity.GetUserName();
            var activityBookings = db.PackageBookings.Include(a => a.PackageTime);
            return View(activityBookings.ToList().Where(x => x.CustomerEmail == userName));
        }

        // GET: ActivityBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageBooking activityBooking = db.PackageBookings.Find(id);
            if (activityBooking == null)
            {
                return HttpNotFound();
            }
            return View(activityBooking);
        }

        // GET: ActivityBookings/Create
        public ActionResult Create(int? id)
        {
            Session["TimePackageId"] = id;
            ViewBag.PackageTimeId = new SelectList(db.PackageTimes1, "PackageTimeId", "PackageTimeId");
            return View();
        }

        // POST: ActivityBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PackageBookingId,ArrivalDate,numOfHours,BasicPrice,CustomerEmail,CustomerName,CustomerLastName,Status,AccomodationPackage,TimeSlot,PackageTimeId")] PackageBooking packageBooking)
        {
            var userName = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                if (packageBooking.CheckBooking(packageBooking) == false)
                {
                    if (packageBooking.ArrivalDate >= DateTime.Now.Date)
                    {
                        packageBooking.PackageTimeId = int.Parse(Session["TimePackageId"].ToString());
                        packageBooking.BasicPrice = packageBooking.getActivityPrice();
                        packageBooking.AccomodationPackage = packageBooking.getPackage();
                        packageBooking.TimeSlot = packageBooking.getTimeSlot();
                        packageBooking.CustomerEmail = userName;
                        packageBooking.CustomerName = packageBooking.getCustomerName(userName);
                        packageBooking.CustomerLastName = packageBooking.getCustomerLastName(userName);
                        db.PackageBookings.Add(packageBooking);
                        db.SaveChanges();
                        PackageBooking.SendEmail(packageBooking);
                        TempData["AlertMessage"] = "Thank you for your booking. \n An Email has been sent with your bookijng details";
                        return RedirectToAction("Index2");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Can't book for a date that has already passed!!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Time already booked for, Please try other time slots!!");
                }

            }

            ViewBag.PackageTimeId = new SelectList(db.PackageTimes1, "ActivityTimeId", "ActivityTimeId", packageBooking.PackageTimeId);
            return View(packageBooking);
        }

        // GET: ActivityBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageBooking activityBooking = db.PackageBookings.Find(id);
            if (activityBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityTimeId = new SelectList(db.PackageTimes1, "PackageTimeId", "ActivityTimeId", activityBooking.PackageTimeId);
            return View(activityBooking);
        }

        // POST: ActivityBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PackageBookingId,ArrivalDate,numOfHours,BasicPrice,CustomerEmail,CustomerName,CustomerLastName,Status,AccomodationPackage,TimeSlot,PackageTimeId")] PackageBooking activityBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityTimeId = new SelectList(db.PackageTimes1, "packageTimeId", "PackageTimeId", activityBooking.PackageTimeId);
            return View(activityBooking);
        }

        // GET: ActivityBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageBooking activityBooking = db.PackageBookings.Find(id);
            if (activityBooking == null)
            {
                return HttpNotFound();
            }
            return View(activityBooking);
        }

        // POST: ActivityBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PackageBooking activityBooking = db.PackageBookings.Find(id);
            db.PackageBookings.Remove(activityBooking);
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