using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Paradise
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AboutUs",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "AboutUs", action = "AboutUs", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Program",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Program", action = "Program", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
