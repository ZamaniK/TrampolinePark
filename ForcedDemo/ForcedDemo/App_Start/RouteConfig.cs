using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ForcedDemo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Cart", "Cart/{action}/{id}", new { controller = "Cart", action = "Index", id = UrlParameter.Optional }, new[] { "ForcedDemo.Controllers" });

            routes.MapRoute("Shop", "Shop/{action}/{name}", new { controller = "Shop", action = "Index", name = UrlParameter.Optional }, new[] { "ForcedDemo.Controllers" });

            routes.MapRoute("SidebarPartial", "Pages/SidebarPartial", new { controller = "Pages", action = "SidebarPartial" }, new[] { "ForcedDemo.Controllers" });
            routes.MapRoute("PagesMenuPartial", "Pages/PagesMenuPartial", new { controller = "Pages", action = "PagesMenuPartial" }, new[] { "ForcedDemo.Controllers" });
            routes.MapRoute("Pages", "{page}", new { controller = "Pages", action = "Index" }, new[] { "ForcedDemo.Controllers" });

            routes.MapRoute(
            name: "FEAccomodations",
            url: "Accomodations",
            defaults: new { area = "ForcedDemo", controller = "Accomodations", action = "Index" },
            namespaces: new[] { "ForcedDemo.Controllers" }
        );

            routes.MapRoute(
              name: "CheckAvailability",
              url: "accomodation-package/{accomodationPackageID}",
              defaults: new { area = "", controller = "Accomodations", action = "Details" },
              namespaces: new[] { "ForcedDemo.Controllers" }
          );

            routes.MapRoute(
              name: "AccomodationPackageDetails",
              url: "accomodation-check-availability",
              defaults: new { area = "", controller = "Accomodations", action = "CheckAvailability" },
              namespaces: new[] { "ForcedDemo.Controllers" }
          );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { area = "", controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ForcedDemo.Controllers" }
            );
            routes.MapRoute("NewPage", "", new { controller = "Pages", action = "Index" }, new[] { "ForcedDemo.Controllers" });


            routes.MapRoute(
              name: "ActivityTimer",
              url: "{controller}/{action}/{id}",

              defaults: new { area = "", controller = "ActivityTimer", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "ForcedDemo.Controllers" }
          );
        }
    }
}
