using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;
using System.Web.Mvc;
using System.Web.Routing;
using Kara.Framework.Common.Mvc.Helpers;
using System.Configuration;

namespace Kara.Framework.Common.Mvc.Filters
{
    public enum DeviceType { Mobile, Desktop }

    public class DeviceRedirectionFilter : ActionFilterAttribute
    {
        public string RedirectController { get; set; }

        public string RedirectAction { get; set; }

        private string RedirectMobileDomain { get; set; }

        public DeviceRedirectionFilter()           
        {
        
        }
        public DeviceRedirectionFilter(string appSettingsKey)
            : this()
        {
            RedirectMobileDomain = ConfigurationManager.AppSettings[appSettingsKey] as string;           
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (DeviceRedirectionHelper.GetDeviceDisplayMode(filterContext.HttpContext) == DeviceRedirectionHelper.DeviceDisplayModes.Mobile)
            {
                this.RedirectToRoute(filterContext, new { area = "Mobile", controller = this.RedirectController, action = this.RedirectAction });
            }
        }

        private void RedirectToRoute(ActionExecutingContext context, object routeValues)
        {
            var rc = new RequestContext(context.HttpContext, context.RouteData);
            var routeValuesDict = new RouteValueDictionary(routeValues);
            routeValuesDict.Remove("area");
            if (context.RequestContext.HttpContext.Request.QueryString != null)
            {
                var queryString = context.RequestContext.HttpContext.Request.QueryString;
                foreach (string key in context.RequestContext.HttpContext.Request.QueryString.Keys)
                {
                    routeValuesDict[key] = queryString[key].ToString();
                }
            }
        
            var virtualPathData = RouteTable.Routes.GetVirtualPath(rc, routeValuesDict);
            if (virtualPathData != null)
            {
                string url = virtualPathData.VirtualPath;
                if (!string.IsNullOrWhiteSpace(RedirectMobileDomain))
                    url = RedirectMobileDomain + url;
                context.Controller.TempData.Keep();
                context.HttpContext.Response.Redirect(url, false);
            }
        }
    } 
}
