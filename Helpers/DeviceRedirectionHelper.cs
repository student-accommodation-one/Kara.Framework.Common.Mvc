using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebPages;

namespace Kara.Framework.Common.Mvc.Helpers
{
    public class DeviceRedirectionHelper
    {
        public enum DeviceDisplayModes
        {
            Mobile,
            Tablet,
            Desktop
        }
        public static DeviceDisplayModes GetDeviceDisplayMode(HttpContextBase context)
        {
            var cookieName = "DeviceDisplayMode";
            DeviceDisplayModes displayMode = DeviceDisplayModes.Desktop;
            if (context.Request != null && context.Request.Cookies != null && context.Request.Cookies.AllKeys.Contains(cookieName))
            {
                //User Preferred Display Type
                DeviceDisplayModes parsedDisplayMode;
                if (Enum.TryParse<DeviceDisplayModes>(context.Request.Cookies[cookieName].Value, true, out parsedDisplayMode))
                {
                    return parsedDisplayMode;
                }                
            }
            
            if (context.GetOverriddenBrowser().IsMobileDevice)
            {
                displayMode = DeviceDisplayModes.Mobile;
            }
            //User
            //Mozilla/5.0 (Linux; Android 4.2.1; en-us; Nexus 5 Build/JOP40D) AppleWebKit/535.19 (KHTML, like Geck
            if (context.Request != null && !string.IsNullOrWhiteSpace(context.Request.UserAgent))
            {
                var userAgent = context.Request.UserAgent;
                //User Agent Checking
                if (userAgent.IndexOf("Mozilla") > -1 && userAgent.IndexOf("Android") > -1)
                {
                    displayMode = DeviceDisplayModes.Mobile;
                }
            }

            return displayMode;
        }
    }
}
