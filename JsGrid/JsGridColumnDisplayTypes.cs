using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kara.Framework.Common.Helpers;

namespace Kara.Framework.Common.Mvc.JsGrid
{
    public enum JsGridColumnDisplayTypes
    {
        [Code("Text")]
        Text,
        [Code("NavigationLink")]
        NavigationLink,
        [Code("FunctionLink")]
        FunctionLink,
        [Code("ShowInModalWindowLink")]
        ShowInModalWindowLink,
        [Code("HtmlTemplateJavascriptFunction")]
        HtmlTemplateJavascriptFunction,
        [Code("Html")]
        Html,
        [Code("ImagePreview")]
        ImagePreview
    }

    
}
