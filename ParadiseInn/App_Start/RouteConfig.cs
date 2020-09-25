using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ParadiseInn
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "FEAccomodation",
                url: "Accomodation",
                defaults: new { area = "", controller = "Accomodation", action = "Index" },
                namespaces: new[] { "ParadiseInn.Controllers" }
            );

            routes.MapRoute(
                name: "AccomodationPackageDetails",
                url: "accomodation-package/{accomodationPackageId}",
                defaults: new { area = "", controller = "Accomodation", action = "Details" },
                namespaces: new[] { "ParadiseInn.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
