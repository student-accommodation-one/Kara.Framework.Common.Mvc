using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kara.Framework.Common.Helpers;
namespace Kara.Framework.Common.Mvc.JsGrid
{
    public class JsGridCommandColumn : JsGridColumnBase
    {
        public string CommandName { get; set; }

        private JsCommandColumnTypes _commandTypeColumn;
        public JsCommandColumnTypes CommandColumnType
        {
            get
            {
                return _commandTypeColumn;
            }
            set
            {
                _commandTypeColumn = value;
                DisplayName = "";
                CommandName = _commandTypeColumn.GetCode();
            }
        }
    }
}
