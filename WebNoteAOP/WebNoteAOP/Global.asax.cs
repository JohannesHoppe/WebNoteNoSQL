using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.Practices.Unity;

using WebNoteAOP.Code.Unity;

namespace WebNoteAOP
{


    /// <summary>
    /// Application startup
    /// </summary>
    public class MvcApplication : HttpApplication, IUnityContainerAccessor
    {
        #region Unity IoC

        /// <summary>
        /// Gets or sets the Unity container for the current application
        /// </summary>
        public static IUnityContainer Container { get; set; }

        /// <summary>
        /// Gets the Unity container of the application 
        /// </summary>
        IUnityContainer IUnityContainerAccessor.Container
        {
            get { return Container; }
        }

        #endregion

        /// <summary>
        /// Sets up the the default routes
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }); // Parameter defaults
        }

        /// <summary>
        /// Called when the first resource (such as a page) in an ASP.NET application is requested
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            UnityHelper.InitializeContainer();
        }

        /// <summary>
        /// Called once per lifetime of the application before the application is unloaded. 
        /// </summary>
        protected void Application_End(object sender, EventArgs e)
        {
            UnityHelper.CleanUp();
        }
    }
}