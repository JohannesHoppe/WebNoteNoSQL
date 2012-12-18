using System;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

using WebNoteNoSQL.Code.Unity;
using WebNoteNoSQL.Models;

namespace WebNoteNoSQL.Code
{
    using System.Configuration;

    /// <summary>
    /// Fills ViewData with all available categories
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FillViewDataAttribute : ActionFilterAttribute
    {
        public const string KeyAllCategories = "AllCategories";
        public const string KeyDbEngine = "DbEngine";

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewData[KeyAllCategories] = ResolveRepository(filterContext).GetAllCategories();
            filterContext.Controller.ViewData[KeyDbEngine] = ConfigurationManager.AppSettings["DbEngine"];
            base.OnActionExecuted(filterContext);
        }

        private static IWebNoteRepository ResolveRepository(ActionExecutedContext filterContext)
        {
            IUnityContainer container = UnityHelper.ResolveContainerFromHttpContext(filterContext.RequestContext);
            return container.Resolve<IWebNoteRepository>();
        }
    }
}
