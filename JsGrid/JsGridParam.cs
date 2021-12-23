using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Framework.Common.Mvc.JsGrid
{
    public class JsGridParam : JsControlParamBase
    {
        private List<JsGridColumnBase> _columns = new List<JsGridColumnBase>();
        private string _deleteConfirmation = "Delete selected record(s)?";

        private string _readData = string.Empty;
        
        public string ReadData
        {
            get { return _readData; }
            set { _readData = value; }
        }
        private string _readUrl = string.Empty;

        public string ReadUrl
        {
            get { return _readUrl; }
            set { _readUrl = value; }
        }
        private string _updateData = string.Empty;

        public string UpdateData
        {
            get { return _updateData; }
            set { _updateData = value; }
        }
        private string _updateUrl = string.Empty;

        public string UpdateUrl
        {
            get { return _updateUrl; }
            set { _updateUrl = value; }
        }
        private string _deleteData = string.Empty;

        public string DeleteData
        {
            get { return _deleteData; }
            set { _deleteData = value; }
        }
        private string _deleteUrl = string.Empty;

        public string DeleteUrl
        {
            get { return _deleteUrl; }
            set { _deleteUrl = value; }
        }

        private int _defaultPageSize = 20;
        public int DefaultPageSize
        {
            get { return _defaultPageSize; }
            set { _defaultPageSize = value; }
        }

        public List<JsGridColumnBase> Columns
        {
            get
            {
                var gridColumns = _columns.OfType<JsGridColumn>().Where(c => c.DisplayOnGrid == true);
                var commandColumns = _columns.OfType<JsGridCommandColumn>();
                var result = new List<JsGridColumnBase>();
                result.AddRange(gridColumns);
                result.AddRange(commandColumns);
                return result;
            }
            set
            {
                _columns = value;
            }
        }

        public JsGridModel JsGridModel { get; set; }

        public JsGridEditModes EditMode { get; set; }

        public bool IsBatch { get; set; }

        private bool _isServerProcessing = true;

        public bool IsServerProcessing
        {
            get { return _isServerProcessing; }
            set { _isServerProcessing = value; }
        }


        public string DeleteConfirmation
        {
            get { return _deleteConfirmation; }
            set { _deleteConfirmation = value; }
        }

        public bool AnyEditableColumnForBatchEditing
        {
            get {
                if (IsBatch == false) return false;
                    var gridColumns = _columns.OfType<JsGridColumn>().Where(c => c.DisplayOnGrid == true);
                    return gridColumns.Any(g => g.Editable);
                }
        }

        public bool AllowDeletion { get; set; }
    }
}
