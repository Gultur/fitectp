﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContosoUniversity
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Register",
                url: "Register",
                defaults: new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "Logout",
                defaults: new { controller = "Account", action = "Logout" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
