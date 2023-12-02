using ForcedDemo.Models;
using ForcedDemo.Models.ActBooking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ForcedDemo.Controllers
{
    public class ActivityBookingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityBookings
        public ActionResult Index()
        {
            var activityBookings = db.ActivityBookings.Include(a => a.ActivityTime);
            return View(activityBookings.ToList());
        }
        public ActionResult Index2()
        {
            var userName = User.Identity.GetUserName();
            var activityBookings = db.ActivityBookings.Include(a => a.ActivityTime);
            return View(activityBookings.ToList().Where(x => x.CustomerEmail == userName));
        }

        // GET: ActivityBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityBooking activityBooking = db.ActivityBookings.Find(id);
            if (activityBooking == null)
            {
                return HttpNotFound();
            }
            return View(activityBooking);
        }

        // GET: ActivityBookings/Create
        public ActionResult Create(int? id)
        {
            Session["TimeActivityId"] = id;
            ViewBag.ActivityTimeId = new SelectList(db.ActivityTimes1, "ActivityTimeId", "ActivityTimeId");
            return View();
        }

        // POST: ActivityBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivvityBookingId,ArrivalDate,numOfHours,BasicPrice,CustomerEmail,CustomerName,CustomerLastName,Status,ActivityType,TimeSlot,ActivityTimeId")] ActivityBooking activityBooking)
        {
            var userName = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                if (activityBooking.CheckBooking(activityBooking) == false)
                {
                    if (activityBooking.ArrivalDate >= DateTime.Now.Date)
                    {
                        activityBooking.ActivityTimeId = int.Parse(Session["TimeActivityId"].ToString());
                        activityBooking.BasicPrice = activityBooking.getActivityPrice();
                        activityBooking.ActivityType = activityBooking.getActivityType();
                        activityBooking.TimeSlot = activityBooking.getTimeSlot();
                        activityBooking.CustomerEmail = userName;
                        activityBooking.CustomerName = activityBooking.getCustomerName(userName);
                        activityBooking.CustomerLastName = activityBooking.getCustomerLastName(userName);
                        db.ActivityBookings.Add(activityBooking);
                        db.SaveChanges();
                        ActivityBooking.SendEmail(activityBooking);
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

            ViewBag.ActivityTimeId = new SelectList(db.ActivityTimes1, "ActivityTimeId", "ActivityTimeId", activityBooking.ActivityTimeId);
            return View(activityBooking);
        }

        // GET: ActivityBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityBooking activityBooking = db.ActivityBookings.Find(id);
            if (activityBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityTimeId = new SelectList(db.ActivityTimes1, "ActivityTimeId", "ActivityTimeId", activityBooking.ActivityTimeId);
            return View(activityBooking);
        }

        // POST: ActivityBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivvityBookingId,ArrivalDate,numOfHours,BasicPrice,CustomerEmail,CustomerName,CustomerLastName,Status,ActivityType,TimeSlot,ActivityTimeId")] ActivityBooking activityBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityTimeId = new SelectList(db.ActivityTimes1, "ActivityTimeId", "ActivityTimeId", activityBooking.ActivityTimeId);
            return View(activityBooking);
        }

        // GET: ActivityBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityBooking activityBooking = db.ActivityBookings.Find(id);
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
            ActivityBooking activityBooking = db.ActivityBookings.Find(id);
            db.ActivityBookings.Remove(activityBooking);
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