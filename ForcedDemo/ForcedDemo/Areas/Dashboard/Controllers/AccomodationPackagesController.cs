using ForcedDemo.Areas.Dashboard.ViewModels;
using ForcedDemo.Entities;
using ForcedDemo.Models;
using ForcedDemo.Services;
using ForcedDemo.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForcedDemo.Areas.Dashboard.Controllers
{
    public class AccomodationPackagesController : Controller
    {
        AccomodationPackageService accomodationPackageService = new AccomodationPackageService();
        AccomodationTypeService accomodationTypeService = new AccomodationTypeService();
        DashboardService dashboardService = new DashboardService();
        // GET: Dashboard/AccomodationTypes
        public ActionResult Index(string searchTerm, int? accomodationTypeID, int? page)
        {
            AccomodationPackageListingModel model = new AccomodationPackageListingModel();

            int recordSize = 5;
            page = page ?? 1;

            model.SearchTerm = searchTerm;
            model.AccomodationTypeID = accomodationTypeID;

            model.AccomodationPackages = accomodationPackageService.SearchAccomodationPackages(searchTerm, accomodationTypeID, page.Value, recordSize);

            model.AccomodationTypes = accomodationTypeService.GetAllAccomodationTypes();

            var totalRecords = accomodationPackageService.SearchAccomodationPackagesCount(searchTerm, accomodationTypeID);
            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationPackageActionModel model = new AccomodationPackageActionModel();

            if (ID.HasValue)  //we are trying to edit a record
            {
                var accomodationPackage = accomodationPackageService.GetAccomodationPackageByID(ID.Value);

                model.ID = accomodationPackage.ID;
                model.AccomodationTypeID = accomodationPackage.AccomodationTypeID;
                model.Name = accomodationPackage.Name;
                model.Description = accomodationPackage.Description;
                model.NoOfPeople = accomodationPackage.NoOfPeople;
                model.FeePerDay = accomodationPackage.FeePerDay;
                model.AccomodationPackagePictures = accomodationPackageService.GetPicturesByAccomodationPackageID(accomodationPackage.ID);
            }
            model.AccomodationTypes = accomodationTypeService.GetAllAccomodationTypes();
            return PartialView("_Action", model);
        }


        [HttpPost]
        public JsonResult Action(AccomodationPackageActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;
            List<int> pictureIDs = !string.IsNullOrEmpty(model.PictureIDs) ? model.PictureIDs.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>();
            var pictures = dashboardService.GetPicturesByID(pictureIDs);

            if (model.ID > 0)
            {
                var accomodationPackage = accomodationPackageService.GetAccomodationPackageByID(model.ID);

                accomodationPackage.AccomodationTypeID = model.AccomodationTypeID;
                accomodationPackage.Name = model.Name;
                accomodationPackage.Description = model.Description;
                accomodationPackage.NoOfPeople = model.NoOfPeople;
                accomodationPackage.FeePerDay = model.FeePerDay;

                accomodationPackage.AccomodationPackagePictures.Clear();
                accomodationPackage.AccomodationPackagePictures.AddRange(pictures.Select(x => new AccomodationPackagePicture() { AccomodationPackageID = accomodationPackage.ID, PictureID = x.ID }));

                result = accomodationPackageService.UpdateAccomodationPackage(accomodationPackage);
            }
            else
            {
                AccomodationPackage accomodationPackage = new AccomodationPackage();

                accomodationPackage.AccomodationTypeID = model.AccomodationTypeID;
                accomodationPackage.Name = model.Name;
                accomodationPackage.Description = model.Description;
                accomodationPackage.NoOfPeople = model.NoOfPeople;
                accomodationPackage.FeePerDay = model.FeePerDay;

                accomodationPackage.AccomodationPackagePictures = new List<AccomodationPackagePicture>();
                accomodationPackage.AccomodationPackagePictures.AddRange(pictures.Select(x => new AccomodationPackagePicture() { PictureID = x.ID }));

                result = accomodationPackageService.SaveAccomodationPackage(accomodationPackage);
            }


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Package." };
            }
            return json;
        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationPackageActionModel model = new AccomodationPackageActionModel();

            var accomodationPackage = accomodationPackageService.GetAccomodationPackageByID(ID);

            model.ID = accomodationPackage.ID;


            return PartialView("_Delete", model);
        }


        [HttpPost]
        public JsonResult Delete(AccomodationPackageActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var accomodationPackage = accomodationPackageService.GetAccomodationPackageByID(model.ID);



            result = accomodationPackageService.DeleteAccomodationPackage(accomodationPackage);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Packages." };
            }
            return json;
        }

        //Appointment Shandis
        public ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Availability(int? id)
        {


            AccomodationPackage Package = db.AccomodationPackages.Find(id);
            if (Package == null)
            {
                return View("Error");
            }
            AppointmentModel test = new AppointmentModel
            {
                AccomodationPackageId = Convert.ToInt32(id),
            };
            ViewBag.TimeBlockHelper = new SelectList(String.Empty);
            ViewBag.DoctorName = Package.Name;
            return PartialView("_Availability",test);
        }

        //GET: /Doctors/UpcomingAppointments/5
        //[Authorize(Roles = "Doctor")]
        public ActionResult UpcomingAppointments(string SearchString, int? id)
        {
            //int n;
            //  bool isInt = int.TryParse(id, out n);
            //if (!isInt)
            //{
            var Package = db.AccomodationPackages.Find(id);
            //var model = new EditUserViewModel(Package);
            AccomodationPackage Accomodation = db.AccomodationPackages.First(u => u.Name == Package.Name);
            if (Accomodation == null)
            {
                return View("Error");
            }
            if (!String.IsNullOrEmpty(SearchString))
            {
                Accomodation.Appointments = Accomodation.Appointments.Where(s => s.Accomodation.Name.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            Accomodation.Appointments.Sort();
            return PartialView("_UpcomingAppointments", Accomodation);
            // }
            //else
            //{
            //    if (!User.IsInRole("Admin"))
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    DoctorModel doctor = db.Doctors.Find(n);
            //    if (doctor == null)
            //    {
            //        return View("Error");
            //    }
            //    doctor.Appointments.Sort();
            //    return View(doctor);
            //}
        }

       // GET: /Doctors/History
       //[Authorize(Roles = "Admin, Doctor")]
       // public ActionResult History(string id)
       // {
       //     int n;
       //     bool isInt = int.TryParse(id, out n);
       //     if (!isInt)
       //     {
       //         var user = db.Users.First(u => u.Email == id);
       //         var model = new EditUserViewModel(user);
       //         DoctorModel doctor = db.Doctors.First(u => u.Name == user.Name);
       //         if (doctor == null)
       //         {
       //             return View("Error");
       //         }
       //         doctor.Appointments.Sort();
       //         return View(doctor);
       //     }
       //     else
       //     {
       //         if (!User.IsInRole("Admin"))
       //         {
       //             return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
       //         }
       //         DoctorModel doctor = db.Doctors.Find(n);
       //         if (doctor == null)
       //         {
       //             return View("Error");
       //         }
       //         doctor.Appointments.Sort();
       //         return View(doctor);
       //     }
        }
    }
