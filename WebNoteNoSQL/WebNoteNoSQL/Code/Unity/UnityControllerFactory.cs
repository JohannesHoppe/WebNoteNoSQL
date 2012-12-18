using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.Practices.Unity;

namespace WebNoteNoSQL.Code.Unity
{
    /// <summary>
    /// Return a ready injected controller
    /// </summary>
    /// <remarks>
    /// use the request context parameter to  retrieve the current application
    /// instance. it contains an Unity container that is available through IUnityContainerAccessor
    /// </remarks>
    /// <seealso>see: http://blogs.microsoft.co.il/blogs/gilf/archive/2009/02/08/how-to-use-unity-container-in-asp-net-mvc-framework.aspx</seealso>
    public class UnityControllerFactory : IControllerFactory
    {
        /// <summary>
        /// Creates the specified controller by using the specified request context.
        /// Unity will Resolve the controller (and all dependencies) for us
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <exception cref="ArgumentNullException">controllerName is null or empty</exception>
        /// <exception cref="ApplicationException">Unity could not resolve</exception>
        /// <exception cref="HttpException">404 - controller not found</exception>
        /// <returns>injected controller</returns>
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (string.IsNullOrEmpty(controllerName))
            {
                throw new ArgumentNullException("controllerName");
            }

            IUnityContainer container = UnityHelper.ResolveContainerFromHttpContext(requestContext);
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            IEnumerable<Type> controllerTypes = from t in currentAssembly.GetTypes()
                                                where t.Name.ToUpperInvariant() == (controllerName + "Controller").ToUpperInvariant()
                                                select t;

            if (controllerTypes.Any())
            {
                foreach (Type controllerType in
                    controllerTypes.Where(controllerType => typeof(IController).IsAssignableFrom(controllerType)))
                {
                    try
                    {
                        object match = container.Resolve(controllerType);
                        return match as IController;
                    }
                    catch (ResolutionFailedException ex)
                    {
                        throw new ApplicationException(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "Unity could not resolve '{0}'!\r\n\r\n{1}",
                                controllerType,
                                ex));
                    }
                }
            }

            throw new HttpException(
                404,
                string.Format(CultureInfo.InvariantCulture, "HTTP/1.1 404 Not Found - A compatible controller with the name '{0}' could not be found!", controllerName));
        }

        /// <summary>
        /// Releases the specified controller.
        /// </summary>
        /// <param name="controller">The controller to release.
        /// </param>
        public void ReleaseController(IController controller)
        {
            // ReSharper disable RedundantAssignment
            controller = null;

            // ReSharper restore RedundantAssignment
        }
    }
}