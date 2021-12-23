using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Framework.Common.Mvc.JsModal
{
    public class JsModalParam : JsControlParamBase
    {
        private int _width = 800;
        private int _height = 800;
        private string _title = string.Empty;
        private bool _resizeable = false;

        public bool Resizeable
        {
            get { return _resizeable; }
            set { _resizeable = value; }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }      
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
       

    }
}
