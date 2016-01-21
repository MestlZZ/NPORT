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
               name: "News Details",
               url: "news/item/{id}",
               defaults: new { controller = "News", action = "Detailed", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "User Login",
               url: "login",
               defaults: new { controller = "Account", action = "Login" }
           );

            routes.MapRoute(
               name: "About us",
               url: "about",
               defaults: new { controller = "Home", action = "About" }
           );

            routes.MapRoute(
               name: "Contact info",
               url: "info",
               defaults: new { controller = "Home", action = "Contact" }
           );

            routes.MapRoute(
               name: "User Register",
               url: "register",
               defaults: new { controller = "Account", action = "Register" }
           );

            routes.MapRoute(
               name: "User LogOff",
               url: "logoff",
               defaults: new { controller = "Account", action = "Register" }
           );

            routes.MapRoute(
               name: "News Edit",
               url: "news/edit/{id}",
               defaults: new { controller = "News", action = "Edit", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "News Remove",
               url: "news/remove",
               defaults: new { controller = "News", action = "Remove" }
           );

            routes.MapRoute(
               name: "News Add",
               url: "news/add",
               defaults: new { controller = "News", action = "AddNews" }
           );

            routes.MapRoute(
               name: "User panel",
               url: "user/",
               defaults: new { controller = "User", action = "Index" }
           );

            routes.MapRoute(
               name: "User list",
               url: "users",
               defaults: new { controller = "User", action = "UserList" }
           );

            routes.MapRoute(
               name: "User details",
               url: "user/info/{id}",
               defaults: new { controller = "User", action = "Details", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "User remove",
               url: "user/remove/{id}",
               defaults: new { controller = "User", action = "Remove", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Home",
                url: "news",
                defaults: new { controller = "News", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
