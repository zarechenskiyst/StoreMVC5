﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Test_store
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Pages", "{page}", new { controller = "Pages", action = "Index" }, new[] { "Test_store.Controllers" });

            routes.MapRoute("Default", "", new { controller = "Pages", action = "Index" }, new[] { "Test_store.Controllers" });

            routes.MapRoute("PageMenuPartial", "Pages/PageMenuPartial", new { controller = "Pages", action = "PageMenuPartial" }, new[] { "Test_store.Controllers" });

            routes.MapRoute("Shop", "Shop/{action}/{name}", new { controller = "Shop", action = "Index", name=UrlParameter.Optional }, new[] { "Test_store.Controllers" });

            routes.MapRoute("Cart", "Cart/{action}/{id}", new { controller = "Cart", action = "Index", id = UrlParameter.Optional }, new[] { "Test_store.Controllers" });

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
