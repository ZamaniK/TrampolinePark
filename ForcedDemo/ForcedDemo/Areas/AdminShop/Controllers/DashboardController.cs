using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForcedDemo.Areas.AdminShop.Controllers
{
    public class DashboardController : Controller
    {
        // GET: AdminShop/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}