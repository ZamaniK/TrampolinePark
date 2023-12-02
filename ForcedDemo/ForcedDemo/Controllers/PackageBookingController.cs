using ForcedDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;
using ForcedDemo.Models.PackBooking;

namespace ForcedDemo.Controllers
{
    namespace ForcedDemo.Controllers
    {
        public class PackageBookingController : Controller
        {
            // GET: Dashboard/PackageBookings
            private ApplicationDbContext db = new ApplicationDbContext();

            // GET: ActivityBookings
            public ActionResult Index()
            {
                var packageBookings = db.PackageBookings.Include(a => a.PackageTime);
                return View(packageBookings.ToList());
            }
            public ActionResult Index2()
            {
                var userName = User.Identity.GetUserName();
                var packageBookings = db.PackageBookings.Include(a => a.PackageTime);
                return View(packageBookings.ToList().Where(x => x.CustomerEmail == userName));
            }

            // GET: ActivityBookings/Details/5
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PackageBooking packageBookings = db.PackageBookings.Find(id);
                if (packageBookings == null)
                {
                    return HttpNotFound();
                }
                return View(packageBookings);
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
            public ActionResult Create([Bind(Include = "PackageBookingId,ArrivalDate,numOfHours,BasicPrice,CustomerEmail,CustomerName,CustomerLastName,Status,AccomodationPackage,AccomodationType,TimeSlot,PackageTimeId")] PackageBooking packageBookings)
            {
                var userName = User.Identity.GetUserName();
                if (ModelState.IsValid)
                {
                    if (packageBookings.CheckBooking(packageBookings) == false)
                    {
                        if (packageBookings.ArrivalDate >= DateTime.Now.Date)
                        {
                            packageBookings.PackageTimeId = int.Parse(Session["TimePackageId"].ToString());
                            packageBookings.BasicPrice = packageBookings.getActivityPrice();
                            packageBookings.AccomodationPackage = packageBookings.getPackage();
                            packageBookings.AccomodationType = packageBookings.getPackageType();
                            packageBookings.TimeSlot = packageBookings.getTimeSlot();
                            packageBookings.CustomerEmail = userName;
                            packageBookings.CustomerName = packageBookings.getCustomerName(userName);
                            packageBookings.CustomerLastName = packageBookings.getCustomerLastName(userName);
                            db.PackageBookings.Add(packageBookings);
                            db.SaveChanges();
                            PackageBooking.SendEmail(packageBookings);
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

                ViewBag.PackageTimeId = new SelectList(db.PackageTimes1, "ActivityTimeId", "ActivityTimeId", packageBookings.PackageTimeId);
                return View(packageBookings);
            }

            // GET: ActivityBookings/Edit/5
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PackageBooking packageBookings = db.PackageBookings.Find(id);
                if (packageBookings == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ActivityTimeId = new SelectList(db.PackageTimes1, "PackageTimeId", "ActivityTimeId", packageBookings.PackageTimeId);
                return View(packageBookings);
            }

            // POST: ActivityBookings/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "PackageBookingId,ArrivalDate,numOfHours,BasicPrice,CustomerEmail,CustomerName,CustomerLastName,Status,AccomodationPackage,AccomodationType,TimeSlot,PackageTimeId")] PackageBooking packageBookings)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(packageBookings).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ActivityTimeId = new SelectList(db.PackageTimes1, "packageTimeId", "PackageTimeId", packageBookings.PackageTimeId);
                return View(packageBookings);
            }

            // GET: ActivityBookings/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PackageBooking packageBookings = db.PackageBookings.Find(id);
                if (packageBookings == null)
                {
                    return HttpNotFound();
                }
                return View(packageBookings);
            }

            // POST: ActivityBookings/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                PackageBooking packageBookings = db.PackageBookings.Find(id);
                db.PackageBookings.Remove(packageBookings);
                db.SaveChanges();
                return RedirectToAction("Index2");
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
}