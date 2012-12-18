namespace WebNoteAOP.Code.Unity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.Practices.Unity;

    using WebNoteAOP.Models;
    using WebNoteAOP.Models.WebNote;
    using WebNoteAOP.Models.WebNoteCategory;

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
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Never happens!")]
        private static IUnityContainer ContainerSetup()
        {
            IUnityContainer container = new UnityContainer()

                .RegisterType<IWebNote, WebNote>()
                .RegisterType<IWebNoteRepository, WebNoteRepository>()

                .RegisterType<IWebNoteCategory, WebNoteCategory>()
                .RegisterType<IWebNoteCategoryRepository, WebNoteCategoryRepository>()

                .RegisterType<IWebNoteService, WebNoteService>(new PerCallContextLifetimeManager());

                //// UNDONE: StackOverflowDetectionAspect --> creates a stack overlow when unloading the application
                //// container.RegisterInstance(container);

            return container;
        }
    }
}
