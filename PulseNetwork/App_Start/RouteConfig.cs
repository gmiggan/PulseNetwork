using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PulseNetwork
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

         // routes.MapRoute(
         //      "Question",                                           // Route name
         //      "{controller}/{action}/{id}",                            // URL with parameters
         //      new { controller = "Home", action = "QuestionView", id = "" }  // Parameter defaults
         //  );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );



            routes.MapRoute(
           name: "Hello",
           url: "{controller}/{action}/{name}/{id}"
            );
        }
    }
}
