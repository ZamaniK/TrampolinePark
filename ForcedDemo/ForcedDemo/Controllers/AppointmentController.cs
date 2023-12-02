using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using ForcedDemo.Models;
using static ForcedDemo.Models.AppointmentModel;
using ForcedDemo.Entities;

namespace ForcedDemo.Controllers
{
    
        public class AppointmentController : Controller
        {
            private ApplicationDbContext db = new ApplicationDbContext();


            // GET: /Appointments/Details/5
            [Authorize]
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AppointmentModel appointment = db.Appointments.Find(id);
                if (appointment == null)
                {
                    return View("Error");
                }
                return View(appointment);
            }

            // GET: /Appointments/Create
            //[Authorize(Roles = "Admin")]
            public ActionResult Create(int? id)
            {
                Session["docId"] = id;

                ViewBag.DoctorID = new SelectList(db.AccomodationPackages.Where(x => x.ID == id), "ID", "Name");
                ViewBag.TimeBlockHelper = new SelectList(String.Empty);
                Accomodation doctor = db.Accomodations.Find(id);
                var model = new AppointmentModel
                {
                    AccomodationPackageId = Convert.ToInt32(id),
                    UserID = User.Identity.GetUserId()
                };
            ViewBag.DoctorName = doctor.Name;
            //ViewBag.Picture = doctor.AccomodationPictures;
            return View(model);
            }

            


            //// POST: /Appointments/Create
            //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[HttpPost]
            ////[Authorize(Roles = "Patient")]
            //[ValidateAntiForgeryToken]
            //public async Task<ActionResult> Create([Bind(Include = "AppointmentID,DoctorID,Date,TimeBlockHelper,Time")] AppointmentModel appointment)
            //{


            //    //Add userID

            //    appointment.UserID = User.Identity.GetUserId();
            //    var doctor = Session["docId"].ToString();
            //    appointment.AccomodationPackageId = int.Parse(doctor);
            //    //Verify Time
            //    if (appointment.TimeBlockHelper == "DONT")
            //        ModelState.AddModelError("", "No Appointments Available for " + appointment.Date.ToShortDateString());
            //    else
            //    {
            //        //Set Time
            //        appointment.Time = DateTime.Parse(appointment.TimeBlockHelper);

            //        //CheckWorkingHours
            //        DateTime start = appointment.Date.Add(appointment.Time.TimeOfDay);
            //        DateTime end = (appointment.Date.Add(appointment.Time.TimeOfDay)).AddMinutes(double.Parse(db.Administrations.Find(1).Value));
            //        if (!(BusinessLogic.IsInWorkingHours(start, end)))
            //            ModelState.AddModelError("", "Doctor Working Hours are from " + int.Parse(db.Administrations.Find(2).Value) + " to " + int.Parse(db.Administrations.Find(3).Value));

            //        //Check Appointment Clash
            //        string check = BusinessLogic.ValidateNoAppoinmentClash(appointment);
            //        if (check != "")
            //            ModelState.AddModelError("", check);
            //    }

            //    //Continue Normally
            //    if (ModelState.IsValid)
            //    {

            //        var lavo = db.Appointments.ToList().Where(p => p.UserID == User.Identity.GetUserName()).Select(p => p.UserID).FirstOrDefault();
            //        Email email = new Email();
            //        email.SendConfirmation(User.Identity.GetUserName(), lavo, appointment.Date, appointment.Time);

            //        //  appointment.BookingPrice = appointment.CalcBookingPrice();
            //        TempData["AlertMessege"] = "Appointent Successfully Made.";
            //        db.Appointments.Add(appointment);
            //        db.SaveChanges();
            //        return RedirectToAction("Details", new { Controller = "RegisteredUsers", Action = "Details" });
            //    }

            //    //Fill Neccessary ViewBags
            //    //ViewBag.DoctorID = new SelectList(db.Doctors.Where(x => x.DisableNewAppointments == false), "ID", "Name", appointment.DoctorID);
            //    ViewBag.TimeBlockHelper = new SelectList(String.Empty);
            //    return View(appointment);
            //}


            
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //[Authorize]

            //public ActionResult DoctorRefferall([Bind(Include = "AppointmentID,DoctorID,Date,TimeBlockHelper,Available,ProcedurID")] AppointmentModel appointment)
            //{

            //    //Verify Time
            //    if (appointment.TimeBlockHelper == "DONT")
            //        ModelState.AddModelError("", "No Appointments Available for " + appointment.Date.ToShortDateString());
            //    else
            //    {
            //        //Set Time
            //        appointment.Time = DateTime.Parse(appointment.TimeBlockHelper);
            //        //Check WorkingHours
            //        DateTime start = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day, appointment.Time.Hour, appointment.Time.Minute, appointment.Time.Second);
            //        DateTime end = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day, appointment.Time.Hour, appointment.Time.Minute, appointment.Time.Second).AddMinutes(double.Parse(db.Administrations.Find(1).Value));
            //        if (!BusinessLogic.IsInWorkingHours(start, end))
            //            ModelState.AddModelError("", "Doctor Working Hours are from " + int.Parse(db.Administrations.Find(2).Value) + " to " + int.Parse(db.Administrations.Find(3).Value));
            //    }

            //    //Continue
            //    if (ModelState.IsValid)
            //    {


            //        db.Entry(appointment).State = EntityState.Modified;
            //        db.Entry(appointment).Property(i => i.UserID).IsModified = false;
            //        db.SaveChanges();




            //        if (User.IsInRole("Admin"))
            //        {
            //            return RedirectToAction("Index");
            //        }
            //        return RedirectToAction("UpcomingAppointments", "Doctor");
            //    }
            //    ViewBag.TimeBlockHelper = new SelectList(String.Empty);
            //    //ViewBag.DoctorID = new SelectList(db.Doctors.Where(x => x.DisableNewAppointments == false), "ID", "Name", appointment.DoctorID);
            //    return View(appointment);
            //}
            // GET: /Appointments/Edit/5
            //[Authorize(Roles = "Admin, Patient")]
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AppointmentModel appointment = db.Appointments.Find(id);
                if (appointment == null)
                {
                    return View("Error");
                }
                ViewBag.TimeBlockHelper = new SelectList(String.Empty);
                ViewBag.Date = appointment.Date.Date;
                //ViewBag.DoctorID = new SelectList(db.Doctors.Where(x => x.DisableNewAppointments == false), "ID", "Name", appointment.DoctorID);
                return View(appointment);
            }

            // POST: /Appointments/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            [Authorize]
            public ActionResult Edit([Bind(Include = "AppointmentID,DoctorID,Date,TimeBlockHelper,Available")] AppointmentModel appointment)
            {
                //Verify Time
                if (appointment.TimeBlockHelper == "DONT")
                    ModelState.AddModelError("", "No Appointments Available for " + appointment.Date.ToShortDateString());
                else
                {
                    //Set Time
                    appointment.Time = DateTime.Parse(appointment.TimeBlockHelper);
                    //Check WorkingHours
                    DateTime start = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day, appointment.Time.Hour, appointment.Time.Minute, appointment.Time.Second);
                    DateTime end = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day, appointment.Time.Hour, appointment.Time.Minute, appointment.Time.Second).AddMinutes(double.Parse(db.Administrations.Find(1).Value));
                    if (!BusinessLogic.IsInWorkingHours(start, end))
                        ModelState.AddModelError("", "Doctor Working Hours are from " + int.Parse(db.Administrations.Find(2).Value) + " to " + int.Parse(db.Administrations.Find(3).Value));
                }

                //Continue
                if (ModelState.IsValid)
                {
                    db.Entry(appointment).State = EntityState.Modified;
                    db.Entry(appointment).Property(u => u.UserID).IsModified = false;
                    db.SaveChanges();
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Details", new { Controller = "RegisteredUsers", Action = "Details" });

                }
                ViewBag.TimeBlockHelper = new SelectList(String.Empty);
                //ViewBag.DoctorID = new SelectList(db.Doctors.Where(x => x.DisableNewAppointments == false), "ID", "Name", appointment.DoctorID);
                return View(appointment);
            }

            // GET: /Appointments/Delete/5
            [Authorize]
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AppointmentModel appointment = db.Appointments.Find(id);
                if (appointment == null)
                {
                    return View("Error");
                }
                return View(appointment);
            }


           

            // POST: /Appointments/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                AppointmentModel holidays = db.Appointments.Find(id);
                db.Appointments.Remove(holidays);
                db.SaveChanges();
                return RedirectToAction("PatientIndex");
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }

            //Here or in model?
            [HttpPost]
            public JsonResult GetAvailableAppointments(int docID, DateTime date)
            {
                List<SelectListItem> resultlist = BusinessLogic.AvailableAppointments(docID, date);
                return Json(resultlist);
            }
        }
    }

