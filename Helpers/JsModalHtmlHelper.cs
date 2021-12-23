using Kara.Framework.Common.Mvc.JsModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Kara.Framework.Common.Mvc.Helpers
{
    public static class JsModalHtmlHelper
    {
        public static MvcHtmlString JsModal(this HtmlHelper htmlHelper, JsModalParam param)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(@"<script type=""text/javascript"">");
            sb.AppendLine(@"        var [JsModalParam.JsNamespace] = {};");
            sb.AppendLine(@"");
            sb.AppendLine(@"        [JsModalParam.JsNamespace].onClose = function() {");
            var onCloseFunctions = param.EventFunctions.Where(f => f.EventName.ToLower() == "onclose");
            foreach (var eventFunction in onCloseFunctions)
                sb.AppendFormat(@"            {0}();", eventFunction.FunctionName);
            sb.AppendLine(@"        };");
            sb.AppendLine(@"");
            sb.AppendLine(@" $(document).ready(function () {");
            sb.AppendLine(@"        [JsModalParam.JsNamespace].modalWindow = $(""#[JsModalParam.PlaceHolderId]"");");
            sb.AppendLine(@"        [JsModalParam.JsNamespace].modalWindow.kendoWindow({");
            sb.AppendLine(@"            actions: [""Maximize"", ""Close""],");
            sb.AppendLine(@"            draggable: true,");
            sb.AppendLine(@"            height: ""[JsModalParam.Height]px"",");
            sb.AppendLine(@"            modal: true,");
            sb.AppendLine(@"            resizable: [JsModalParam.Resizeable],");
            sb.AppendLine(@"            title: ""[JsModalParam.Title]"",");
            sb.AppendLine(@"            width: ""[JsModalParam.Width]px"",");
            sb.AppendLine(@"            visible: false, ");
            sb.AppendLine(@"            close: [JsModalParam.JsNamespace].onClose ");
            sb.AppendLine(@"        });");
            sb.AppendLine(@"    });;");
            sb.AppendLine(@"");
            sb.AppendLine(@"        [JsModalParam.JsNamespace].open = function() {");
            sb.AppendLine(@"            [JsModalParam.JsNamespace].modalWindow.data(""kendoWindow"").center().open();");
            sb.AppendLine(@"        };");
            sb.AppendLine(@"");
            sb.AppendLine(@"        [JsModalParam.JsNamespace].close = function() {");
            sb.AppendLine(@"            [JsModalParam.JsNamespace].modalWindow.data(""kendoWindow"").close();");
            sb.AppendLine(@"        };");
            sb.AppendLine(@"</script>");
            var script = sb.ToString();
            script = JsControlHelper.ReplaceParamWithValue(typeof(JsModalParam), param, script);

            return MvcHtmlString.Create(script);
        }
    }
}
