using Kara.Framework.Common.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Kara.Framework.Common.Mvc.Filters
{
    [AttributeUsageAttribute(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class JsGridGetRequestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            filterContext.Controller.ViewBag.JsGridGetRequest = new JsGridGetRequest(filterContext.Controller.ControllerContext.HttpContext.Request);
        }
    }
}
