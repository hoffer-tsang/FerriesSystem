/*==============================================================================
 *
 * The start up class
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Ferry.App_Start.StartUp))]

namespace Ferry.App_Start
{
    /// <summary>
    /// the start up class
    /// </summary>
    public class StartUp
    {
        /// <summary>
        /// the configuration setting
        /// </summary>
        /// <param name="app"> the current app builder </param>
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/login"),
                LogoutPath = new PathString("/logoff"),
                ExpireTimeSpan = TimeSpan.FromMinutes(30.0),
                CookieSecure = Microsoft.Owin.Security.Cookies.CookieSecureOption.Always
            });
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
