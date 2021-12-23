using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Kara.Framework.Common.Mvc.Filters
{
    public class RequireSslAttribute : RequireHttpsAttribute
    {
        public bool RequireSsl { get; set; }
   
        public RequireSslAttribute()
        {           
            RequireSsl = true;
        }
    
        public RequireSslAttribute(string appSettingsKey): this()
        {
            string key = ConfigurationManager.AppSettings[appSettingsKey] as string;
            if (!string.IsNullOrEmpty(key))
            {
                key = key.ToLower();
                if (key == "false" || key == "0")
                    RequireSsl = false;
            }
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext != null &&
            RequireSsl &&
            !filterContext.HttpContext.Request.IsSecureConnection)
            {
                HandleNonHttpsRequest(filterContext);
            }
        }
    } 
}
