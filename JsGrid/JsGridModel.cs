using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Framework.Common.Mvc.JsGrid
{
    public class JsGridModel
    {
        private List<JsGridColumn> _fields = new List<JsGridColumn>();
        public string Id { get; set; }
        public List<JsGridColumn> Fields
        {
            get {
                return _fields.OfType<JsGridColumn>().ToList();
            }
            set {
                _fields = value;
            }
        }
    }
}
