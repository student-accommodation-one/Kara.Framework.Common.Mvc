using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Kara.Framework.Common.Mvc.ActionResults
{
    public class ImageResult : ActionResult
    {
        public String ContentType { get; set; }
        public byte[] ImageBytes { get; set; }
        public String SourceFilename { get; set; }

        //This is used for times where you have a physical location
        public ImageResult(String sourceFilename, String contentType)
        {
            SourceFilename = sourceFilename;
            ContentType = contentType;
        }

        public ImageResult(byte[] sourceStream, String contentType)
        {
            ImageBytes = sourceStream;
            ContentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            //response.Cache.SetCacheability(HttpCacheability.NoCache);
            //response.Cache.SetCacheability(HttpCacheability.Private);
            //response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(30));
            response.ContentType = ContentType;

            if (ImageBytes != null)
            {
                using(var stream = new MemoryStream(ImageBytes))
                    stream.WriteTo(response.OutputStream);
            }
            else
            {
                response.TransmitFile(SourceFilename);
            }
        }
    }
}
