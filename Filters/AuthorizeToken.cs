using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Kara.Framework.Common.Mvc.Filters
{
    public class AuthorizeToken : AuthorizeAttribute
    {
        private const string TokenName = "kt";
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {            
 	        var provider = ServiceLocator.Current.GetInstance<ISessionTokenProvider>();
            var token = string.Empty;
            if (httpContext.Request.QueryString.AllKeys.Any(k => k == TokenName))
            {
                token = httpContext.Request.QueryString[TokenName].ToString();
            }
            if (string.IsNullOrWhiteSpace(token) && httpContext.Request.Form.AllKeys.Any(k => k == TokenName))
            {
                token = httpContext.Request.Form[TokenName].ToString();
            }
            if (string.IsNullOrWhiteSpace(token))
                return false;

            return provider.ValidateToken(token);  
        }
    }
}
