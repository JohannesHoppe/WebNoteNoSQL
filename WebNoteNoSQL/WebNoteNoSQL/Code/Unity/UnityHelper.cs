using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.Practices.Unity;

using WebNoteNoSQL.Code.DbStartScript;
using WebNoteNoSQL.Models;
using WebNoteNoSQL.Models.EntityFramework;
using WebNoteNoSQL.Models.MongoDb;
using WebNoteNoSQL.Models.RavenDb;

namespace WebNoteNoSQL.Code.Unity
{
    using WebNoteNoSQL.Models.Redis;

    /// <summary>
    /// Classes for the work with the Unity IoC Framework
    /// </summary>
    public static class UnityHelper
    {
        /// <summary>
        /// Sets up the MvcApplication
        /// </summary>
        public static void InitializeContainer()
        {
            if (MvcApplication.Container != null)
            {
                return;
            }

            MvcApplication.Container = ContainerSetup();

            // setup the controller factory of this application
            ControllerBuilder.Current.SetControllerFactory(typeof(UnityControllerFactory));
        }

        /// <summary>
        /// Disposes the container
        /// </summary>
        public static void CleanUp()
        {
            if (MvcApplication.Container != null)
            {
                MvcApplication.Container.Dispose();
            }
        }

        /// <summary>
        /// Extract the Unity Conainter from the given requestContext.HttpContext.ApplicationInstance which MUST implement IUnityContainerAccessor
        /// </summary>
        /// <param name="requestContext">actual request</param>
        /// <remarks>ApplicationInstance is in fact the MvcApplication, we use a cast to acess the Container-Property</remarks>
        /// <exception cref="ApplicationException">Could not resolve the Unity Container...</exception>
        /// <seealso cref="UnityControllerFactory.CreateController"/>
        /// <returns>unity container</returns>
        public static IUnityContainer ResolveContainerFromHttpContext(RequestContext requestContext)
        {
            IUnityContainerAccessor unityContainerAccessor = requestContext.HttpContext.ApplicationInstance as IUnityContainerAccessor;
            if (unityContainerAccessor != null)
            {
                return unityContainerAccessor.Container;
            }

            throw new ApplicationException("Could not resolve the Unity Container!");
        }

        /// <summary>
        /// Registers types
        /// </summary>
        /// <returns>ready set up container</returns>
        private static IUnityContainer ContainerSetup()
        {
            IUnityContainer container = new UnityContainer();

            string engine = ConfigurationManager.AppSettings["DbEngine"];

            switch (engine)
            {
                case "MongoDB":
                    container.RegisterType<IWebNoteRepository, MongoDbRepository>()
                             .RegisterInstance(typeof(DatabaseStartScript), DatabaseStartScript.ForMongoDb());
                    break;

                case "Redis":
                    container.RegisterType<IWebNoteRepository, RedisRepository>()
                             .RegisterInstance(typeof(DatabaseStartScript), DatabaseStartScript.ForRedis());
                    break;

                case "RavenDB":
                    container.RegisterType<IWebNoteRepository, RavenDbRepository>()
                             .RegisterInstance(typeof(DatabaseStartScript), DatabaseStartScript.ForRavenDb());
                    break;

                default:
                    container.RegisterType<IWebNoteRepository, EntityFrameworkRepository>()
                             .RegisterInstance(typeof(DatabaseStartScript), DatabaseStartScript.ForSqlServer());
                    break;
            }

            container.RegisterType<DatabaseStartScript>();

            return container;
        }
    }
}
