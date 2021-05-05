using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UI
{
    public class RouteConfig
    {
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //    routes.MapRoute(
        //        name: null, 
        //        url: "",
        //        defaults: new { controller = "Candy", action = "List", category = (string)null, page = 1}
        //    );

        //    routes.MapRoute(
        //        name: null,
        //        url: "Page{page}",
        //        defaults: new { controller = "Candy", action = "List", category = (string)null },
        //        constraints: new { page = @"\d+" }
        //    );

        //    routes.MapRoute(
        //        name: null,
        //        url : "{category}",
        //        defaults: new { controller = "Candy", action = "List", page = 1 }
        //    );

        //    routes.MapRoute(
        //        name: null,
        //        url: "{category}/Page{page}",
        //        defaults: new { controller = "Candy", action = "List" },
        //        constraints: new { page = @"\d+" }
        //    );

        //    routes.MapRoute(
        //        name: null, 
        //        url: "{controller}/{action}"
        //    );
        //}
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "",
                new
                {
                    controller = "Candy",
                    action = "List",
                    category = (string)null,
                    page = 1
                }
            );

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Candy", action = "List", category = (string)null },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(null,
                "{category}",
                new { controller = "Candy", action = "List", page = 1 }
            );

            routes.MapRoute(null,
                "{category}/Page{page}",
                new { controller = "Candy", action = "List" },
                new { page = @"\d+" }
            );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
