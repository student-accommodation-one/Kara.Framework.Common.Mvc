using Kara.Framework.Common.Mvc.JsGrid;
using Kara.Framework.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Kara.Framework.Common.Mvc.Helpers
{
   
    public static class JsGridHtmlHelper
    {
        private const string JsGridLinkClassName = "jsGridLink";

        public static MvcHtmlString JsGrid(this HtmlHelper htmlHelper, JsGridParam param)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(@"<script type=""text/javascript"">");
            sb.AppendLine(@"    var [JsGridParam.JsNamespace] = {};");
            sb.AppendLine(@"    [JsGridParam.JsNamespace].initialized = false;");
            sb.AppendLine(@"");
            sb.AppendLine(@"    [JsGridParam.JsNamespace].gridColumns = [");
            for (int i = 0; i < param.Columns.Count; i++)
            {
                var column = param.Columns[i];

                string columnScript = string.Empty;
                if (column is JsGridColumn)
                {
                    var gridColumn = column as JsGridColumn;
                    columnScript = @"        { title: ""[JsGridColumn.DisplayName]"", field: ""[JsGridColumn.FieldName]"", filterable: [JsGridColumn.Filterable], width: [JsGridColumn.Width]";
                    switch (gridColumn.DisplayType)
                    {
                        case JsGridColumnDisplayTypes.Text:
                            break;
                        case JsGridColumnDisplayTypes.NavigationLink:
                            columnScript += ", " + string.Format(@"template: ""<a href='{0}/#={1}#' class='{3}'>#={2}#</a>""", gridColumn.NavigationLinkBaseUrl, gridColumn.NavigationLinkIdFieldName, gridColumn.FieldName, JsGridLinkClassName);
                            break;
                        case JsGridColumnDisplayTypes.FunctionLink:
                            break;
                        case JsGridColumnDisplayTypes.ShowInModalWindowLink:
                            columnScript += ", " + string.Format(@"template: ""<a href='javascript:Global.showModalWindow(&quot;{4}&quot;,&quot;{0}/#={1}#&quot;,&quot;{5}&quot;,&quot;{6}&quot;)' class='{3}'>#={2}#</a>""",
                            gridColumn.NavigationLinkBaseUrl, gridColumn.NavigationLinkIdFieldName, gridColumn.FieldName, JsGridLinkClassName, gridColumn.ModalTitle, gridColumn.ModalWidth, gridColumn.ModalHeight);
                            break;
                        case JsGridColumnDisplayTypes.HtmlTemplateJavascriptFunction:
                            columnScript += ", " + string.Format(@"template: ""#={0}#""", gridColumn.JavascriptFunction);
                            break;
                        case JsGridColumnDisplayTypes.ImagePreview:
                            columnScript += ", " + string.Format(@"template: ""<div data-container='body' data-karatype='popover' data-toggle='popover' data-placement='left' data-trigger='hover' data-content='<img src=&quot;#={0}#&quot;/>'>Preview</div>""", gridColumn.FieldName);
                            break;
                        case JsGridColumnDisplayTypes.Html:
                            columnScript += ", encoded: false ";
                            break;
                        default:
                            break;
                    }
                    
                    columnScript += string.IsNullOrWhiteSpace((column as JsGridColumn).TypeFormat) ? "}" : @", format: ""{0:[JsGridColumn.TypeFormat]}""}";
 
                }
                else if (column is JsGridCommandColumn)
                    columnScript = @"        { title: ""[JsGridCommandColumn.DisplayName]"", command: ""[JsGridCommandColumn.CommandName]"", width: [JsGridCommandColumn.Width]}";

                columnScript = JsControlHelper.ReplaceParamWithValue(column.GetType(), column, columnScript);
                sb.AppendLine(columnScript);
                if (i + 1 != param.Columns.Count) sb.Append(",");
            }
            sb.AppendLine(@"    ];");
            sb.AppendLine(@"    [JsGridParam.JsNamespace].initGrid = function () {");
            sb.AppendLine(@"        $(""#[JsGridParam.PlaceHolderId]"").kendoGrid({");
            sb.AppendLine(@"            dataSource: { ");
            sb.AppendLine(@"                transport: {");
            sb.AppendLine(@"                    read: {");
            sb.AppendLine(@"                        url: '[JsGridParam.ReadUrl]',");
            sb.AppendLine(@"                        cache: false,");
            sb.AppendLine(@"                        dataType: 'json',");
            if (!string.IsNullOrWhiteSpace(param.ReadData))
            {
                sb.AppendLine(@"                        data: [JsGridParam.ReadData],");
            }
            sb.AppendLine(@"                        type: 'GET'");
            sb.AppendLine(@"                    },");
            if (!string.IsNullOrWhiteSpace(param.UpdateUrl))
            {
                sb.AppendLine(@"                    update: {");
                sb.AppendLine(@"                        url: '[JsGridParam.UpdateUrl]',");
                sb.AppendLine(@"                        dataType: 'json',");
                if (!string.IsNullOrWhiteSpace(param.UpdateData))
                {
                    sb.AppendLine(@"                        data: { [JsGridParam.UpdateData] },");
                }
                sb.AppendLine(@"                        type: 'POST',");
                sb.AppendLine(@"                        contentType: 'application/json'");
                sb.AppendLine(@"                    },");
            }
            if (!string.IsNullOrWhiteSpace(param.DeleteUrl))
            {
                sb.AppendLine(@"                    destroy: {");
                sb.AppendLine(@"                        url: '[JsGridParam.DeleteUrl]',");
                sb.AppendLine(@"                        dataType: 'json',");
                if (!string.IsNullOrWhiteSpace(param.DeleteData))
                {
                    sb.AppendLine(@"                        data: { [JsGridParam.DeleteData] },");
                }
                sb.AppendLine(@"                        type: 'POST',");
                sb.AppendLine(@"                        contentType: 'application/json'");
                sb.AppendLine(@"                    },");
            }
            sb.AppendLine(@"                    parameterMap: function (options, operation) {");
            sb.AppendLine(@"                        if (operation !== ""read"" && options.models) {");
            sb.AppendLine(@"                             return kendo.stringify(options); // return kendo.stringify(options.models); { models: kendo.stringify(options.models) };");
            sb.AppendLine(@"                        }");
            sb.AppendLine(@"                        return options; ");
            sb.AppendLine(@"                    }");
            sb.AppendLine(@"                },");
            sb.AppendLine(@"                requestEnd: function(e) {");
            sb.AppendLine(@"                    var response = e.response;");
            sb.AppendLine(@"                    var type = e.type;");
            sb.AppendLine(@"                    if (type != ""read"")");
            sb.AppendLine(@"                        [JsGridParam.JsNamespace].refreshGrid();");
            sb.AppendLine(@"                                                ");
            var requestEndFunctions = param.EventFunctions.Where(f => f.EventName.ToLower() == "requestend");
            foreach (var eventFunction in requestEndFunctions)
                sb.AppendFormat(@"            {0}();", eventFunction.FunctionName);
            sb.AppendLine(@"                },");
            sb.AppendLine(@"                schema: {");
            sb.AppendLine(@"                    total: ""TotalRows"",");
            sb.AppendLine(@"                    model: {");
            sb.AppendLine(@"                        id: ""[JsGridModel.Id]"",");
            sb.AppendLine(@"                        fields: {");
            for (int i = 0; i < param.JsGridModel.Fields.Count; i++)
            {
                var field = param.JsGridModel.Fields[i];

                string fieldScript = @"       ""[JsGridColumn.FieldName]"": { type: ""[JsGridColumn.Type]"", editable: [JsGridColumn.Editable]}";

                fieldScript = JsControlHelper.ReplaceParamWithValue(field.GetType(), field, fieldScript);
                sb.AppendLine(fieldScript);
                if (i + 1 != param.JsGridModel.Fields.Count) sb.Append(",");
            }
            sb.AppendLine(@"                        }");
            sb.AppendLine(@"                    },");
            sb.AppendLine(@"                    data: ""Results""");
            sb.AppendLine(@"                },");
            if (param.IsBatch)
                sb.AppendLine(@"                batch: true,");
            sb.AppendLine(@"                pageSize: [JsGridParam.DefaultPageSize],");
            sb.AppendLine(@"                serverPaging: [JsGridParam.IsServerProcessing],");
            sb.AppendLine(@"                serverFiltering: [JsGridParam.IsServerProcessing],");
            sb.AppendLine(@"                serverSorting: [JsGridParam.IsServerProcessing]");
            sb.AppendLine(@"            },");
            sb.AppendLine(@"            filterable: {");
            sb.AppendLine(@"                extra: false,");
            sb.AppendLine(@"                operators: {");
            sb.AppendLine(@"                    number: {");
            sb.AppendLine(@"                        eq: ""Equal to"",");
            sb.AppendLine(@"                        neq: ""Not equal to"",");
            sb.AppendLine(@"	                    gte: ""Greater than or equal to"",");
            sb.AppendLine(@"	                    lte: ""Less than or equal to""");
            sb.AppendLine(@"                    },");
            sb.AppendLine(@"                    string: {");
            sb.AppendLine(@"                        startswith: ""Starts with"",");
            sb.AppendLine(@"	                    endswith: ""Ends with"",");
            sb.AppendLine(@"                        doesnotcontain: ""Doesn't contain"",");
            sb.AppendLine(@"                        contains: ""Contains""");
            sb.AppendLine(@"                    },");
            sb.AppendLine(@"                    date: {");
            sb.AppendLine(@"                        eq: ""Equal to"",");
            sb.AppendLine(@"                        neq: ""Not equal to"",");
            sb.AppendLine(@"	                    gte: ""Greater than or equal to"",");
            sb.AppendLine(@"	                    lte: ""Less than or equal to""");
            sb.AppendLine(@"                    }");
            sb.AppendLine(@"                }");
            sb.AppendLine(@"            },");
            sb.AppendLine(@"            sortable: true,");
            sb.AppendLine(@"            groupable: true,");
            sb.AppendLine(@"            pageable: {");
            sb.AppendLine(@"                refresh: true,");
            sb.AppendLine(@"                pageSizes: [5,10,20,50,100],");
            sb.AppendLine(@"                buttonCount: 5, ");
            sb.AppendLine(@"                input: true");
            sb.AppendLine(@"            },");
            sb.AppendLine(@"            editable: {");
            sb.AppendLine(@"                mode: ""[JsGridParam.EditMode]"",");
            if (param.IsBatch)
                sb.AppendLine(@"                confirmation: false");
            else
                sb.AppendLine(@"                confirmation: ""[JsGridParam.DeleteConfirmation]""");
            sb.AppendLine(@"            },");
            sb.AppendLine(@"            scrollable: false,");
            sb.AppendLine(@"            resizeable: true,");
            if (param.IsBatch)
            {
                if (param.AnyEditableColumnForBatchEditing)
                    sb.AppendLine(@"            toolbar: [""save"", ""cancel"", { name: ""destroy"", text: ""Delete Selected"" }],"); //""create"",
                else if (param.AllowDeletion)
                    sb.AppendLine(@"            toolbar: [{ name: ""destroy"", text: ""Delete Selected"" }],"); //""create"",
               
                sb.AppendLine(@"            selectable: ""multiple, row"",");
            }
            sb.AppendLine(@"            columns: [JsGridParam.JsNamespace].gridColumns");
            sb.AppendLine(@"        });");
            sb.AppendLine(@"    };");
            sb.AppendLine(@" ");
            sb.AppendLine(@"    [JsGridParam.JsNamespace].refreshGrid = function () {");
            sb.AppendLine(@"        $(""#[JsGridParam.PlaceHolderId]"").data(""kendoGrid"").dataSource.read();");
            sb.AppendLine(@"    };");
            sb.AppendLine(@"    $(document).ready([JsGridParam.JsNamespace].initGrid());");
            if (param.IsBatch)
            {
                sb.AppendLine(@"");
                sb.AppendLine(@" $("".k-grid-delete"").click(function (e) {");
                sb.AppendLine(@"        e.preventDefault();");
                sb.AppendLine(@"");
                sb.AppendLine(@"        if (confirm(""[JsGridParam.DeleteConfirmation]""))");
                sb.AppendLine(@"        {");
                sb.AppendLine(@"            var grid = $(""#[JsGridParam.PlaceHolderId]"").data(""kendoGrid"");");
                sb.AppendLine(@"            grid.select().each(function () {");
                sb.AppendLine(@"                grid.removeRow($(this));");
                sb.AppendLine(@"            });");
                sb.AppendLine(@"            grid.saveChanges();");
                sb.AppendLine(@"        }");
                sb.AppendLine(@"");
                sb.AppendLine(@"    });");
            }


            sb.AppendLine(@"</script>");

            //Didn't use string.format due to script readability
            var script = sb.ToString();
            script = JsControlHelper.ReplaceParamWithValue(typeof(JsGridParam), param, script);
            script = JsControlHelper.ReplaceParamWithValue(typeof(JsGridModel), param.JsGridModel, script);
            return MvcHtmlString.Create(script);
        }


    }
}
