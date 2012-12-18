using System;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

using WebNoteAOP.Code.Unity;
using WebNoteAOP.Models;

namespace WebNoteAOP.Code
{
    using System.Configuration;

    /// <summary>
    /// Fills ViewData with all available categories
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FillViewDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewData["AllAvailableCategories"] = Resolve(filterContext).GetAllCategories();
            base.OnActionExecuted(filterContext);
        }

        private static IWebNoteService Resolve(ActionExecutedContext filterContext)
        {
            IUnityContainer container = UnityHelper.ResolveContainerFromHttpContext(filterContext.RequestContext);
            return container.Resolve<IWebNoteService>();
        }
    }
}
