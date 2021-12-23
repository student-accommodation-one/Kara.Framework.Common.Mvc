using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Kara.Framework.Common.Mvc.Filters
{
    public class PublicCacheFilter : ActionFilterAttribute
    {
        public int ExpiresIn { get; set; }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(ExpiresIn));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(true);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetProxyMaxAge(new TimeSpan(0, ExpiresIn, 0));
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.Public);

            base.OnResultExecuting(filterContext);
        }
    }
}
