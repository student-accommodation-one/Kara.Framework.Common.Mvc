using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kara.Framework.Common.Helpers;
namespace Kara.Framework.Common.Mvc.JsGrid
{
    public class JsGridColumn : JsGridColumnBase
    {
        private bool _displayOnGrid = true;

        public bool DisplayOnGrid
        {
            get { return _displayOnGrid; }
            set { _displayOnGrid = value; }
        }

        public string FieldName { get; set; }
        public bool Filterable { get; set; }
        public bool Editable { get; set; }
        public JsGridColumnTypes Type { get; set; }

        private JsGridColumnDisplayTypes _displayType = JsGridColumnDisplayTypes.Text;

       public JsGridColumnDisplayTypes DisplayType
        {
            get { return _displayType; }
            set { _displayType = value; }
        }
    
        public string TypeFormat
        {
            get
            {
                return Type.ToStandardFormat();
            }
        }

        public static JsGridColumnTypes ToJsGridColumnType(Type systemType)
        {
            if (typeof(string) == systemType)
                return JsGridColumnTypes.String;
            else if (typeof(byte) == systemType)
                return JsGridColumnTypes.NonDecimal;
            else if (typeof(short) == systemType)
                return JsGridColumnTypes.NonDecimal;
            else if (typeof(int) == systemType)
                return JsGridColumnTypes.NonDecimal;
            else if (typeof(long) == systemType)
                return JsGridColumnTypes.NonDecimal;
            else if (typeof(double) == systemType)
                return JsGridColumnTypes.Decimal;
            else if (typeof(decimal) == systemType)
                return JsGridColumnTypes.Decimal;
            else if (typeof(DateTime) == systemType)
                return JsGridColumnTypes.DateTime;
            //Money
            return JsGridColumnTypes.String;
        }

        private string _navigationLinkBaseUrl = string.Empty;

        [IgnoreOnParsing]
        public string NavigationLinkBaseUrl
        {
            get { return _navigationLinkBaseUrl; }
            set { _navigationLinkBaseUrl = value; }
        }

        private string _navigationLinkIdFieldName = string.Empty;

        [IgnoreOnParsing]
        public string NavigationLinkIdFieldName
        {
            get { return _navigationLinkIdFieldName; }
            set { _navigationLinkIdFieldName = value; }
        }

        private string _javascriptFunction = string.Empty;
        
        [IgnoreOnParsing]
        public string JavascriptFunction
        {
            get { return _javascriptFunction; }
            set { _javascriptFunction = value; }
        }
        private string _modalTitle = string.Empty;

        public string ModalTitle
        {
            get { return _modalTitle; }
            set { _modalTitle = value; }
        }

        private string _modalWidth = string.Empty;

        public string ModalWidth
        {
            get { return _modalWidth; }
            set { _modalWidth = value; }
        }

        private string _modalHeight = string.Empty;

        public string ModalHeight
        {
            get { return _modalHeight; }
            set { _modalHeight = value; }
        }


    }
}
