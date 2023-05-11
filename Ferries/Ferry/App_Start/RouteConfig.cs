/*==============================================================================
 *
 * Route config of the application 
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System.Web.Mvc;
using System.Web.Routing;

namespace Ferry
{
    /// <summary>
    /// route config class 
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// the regiser routes 
        /// </summary>
        /// <param name="routes"> the route colllection </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
