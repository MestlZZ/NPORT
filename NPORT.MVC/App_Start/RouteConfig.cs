using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NPORT
{
    public class RouteConfig
    {
        public static void RegisterRoutes( RouteCollection routes )
        {
            routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );

            routes.MapRoute(
               name: "News",
               url: "news/item/{id}",
               defaults: new { controller = "Home", action = "Detailed", id = UrlParameter.Optional, }
           );

            routes.MapRoute(
               name: "Edit",
               url: "news/edit/{id}",
               defaults: new { controller = "Home", action = "Edit", id = UrlParameter.Optional, }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
