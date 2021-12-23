using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Framework.Common.Mvc.JsGrid
{
    public abstract class JsGridColumnBase
    {
        private string _displayName = string.Empty;

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }
      
        public int Width { get; set; }
    }
}
