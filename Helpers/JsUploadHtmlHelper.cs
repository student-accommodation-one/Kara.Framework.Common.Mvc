using Kara.Framework.Common.Mvc.JsUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Kara.Framework.Common.Mvc.Helpers
{
    public static class JsUploadHtmlHelper
    {
        public static MvcHtmlString JsUpload(this HtmlHelper htmlHelper, JsUploadParam param)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(@"   <form method=""post"" action=""[JsUploadParam.UploadUrl]"">");
            sb.AppendLine(@"    <div style=""overflow-y:auto"" class=""panel"">");
            sb.AppendLine(@"        <input name=""files"" id=""files"" type=""file"" />");
            sb.AppendLine(@"        <div class=""pull-right"" style=""margin-top:5px;"">");
            sb.AppendLine(@"            <button id=""jsUpload_btnReset"" type=""button"" class=""k-button hidden"">Reset</button>");
            sb.AppendLine(@"        </div>");
            sb.AppendLine(@"    </div>");
            sb.AppendLine(@"    </form>");
            sb.AppendLine(@"");
            sb.AppendLine(@"    <script type=""text/javascript"">");
            sb.AppendLine(@"        var [JsUploadParam.JsNamespace] = {};");
            sb.AppendLine(@"        [JsUploadParam.JsNamespace].btnReset = ""#jsUpload_btnReset"";");
            sb.AppendLine(@"        [JsUploadParam.JsNamespace].reset = function () {");
            sb.AppendLine(@"            $("".k-upload-files.k-reset"").find(""li"").parent().remove();");
            sb.AppendLine(@"        };");
            sb.AppendLine(@"        [JsUploadParam.JsNamespace].showReset = function () {");
            sb.AppendLine(@"            $([JsUploadParam.JsNamespace].btnReset).removeClass(""hidden"");");
            sb.AppendLine(@"        }");
            sb.AppendLine(@"");
            sb.AppendLine(@"        $(document).ready(function () {");
            sb.AppendLine(@"            $(""#files"").kendoUpload({");
            sb.AppendLine(@"                async: {");
            sb.AppendLine(@"                    saveUrl: "" [JsUploadParam.UploadUrl]"",");
            sb.AppendLine(@"                    autoUpload: true");
            sb.AppendLine(@"                },");
            sb.AppendLine(@"                select: [JsUploadParam.JsNamespace].showReset");
            sb.AppendLine(@"            });");
            sb.AppendLine(@"            $([JsUploadParam.JsNamespace].btnReset).bind(""click"", function () {");
            sb.AppendLine(@"                [JsUploadParam.JsNamespace].reset();");
            sb.AppendLine(@"            });");
            sb.AppendLine(@"        });");
            sb.AppendLine(@"");
            sb.AppendLine(@"    </script>");

            var script = sb.ToString();
            script = JsControlHelper.ReplaceParamWithValue(typeof(JsUploadParam), param, script);

            return MvcHtmlString.Create(script);

        }
    }
}
