using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Framework.Common.Mvc.Helpers
{
    public class ImageHelper
    {
        public static string GetImageSource(byte[] image)
        {
            var imgSrc = string.Empty;
            if (image != null)
            {
                var base64 = Convert.ToBase64String(image);
                imgSrc = String.Format("data:image/png;base64,{0}", base64);
            }
            else
                imgSrc = "~/Image/photonotavailable.png";

            return imgSrc;
        }
    }
}
