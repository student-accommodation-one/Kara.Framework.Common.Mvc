using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Framework.Common.Mvc
{
    public class JsControlParamBase
    {
        private List<JsEventFunction> _eventFunctions = new List<JsEventFunction>();

        public string JsNamespace { get; set; }
        public string PlaceHolderId { get; set; }

        public List<JsEventFunction> EventFunctions
        {
            get
            {
                return _eventFunctions;
            }
            set
            {
                _eventFunctions = value;
            }
        }
    }
}
