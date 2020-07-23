using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.RecordTrancation
{
    public class ContractFileType
    {
        [DapperExtensions.Key(true)]
        public int ID { get; set; }

        public string RecordID { get; set; }

        public int RecordTypeID { get; set; }

        public string HoldingCell { get; set; }

        public string OriginalRecordType { get; set; }

        [DapperExtensions.Computed]
        public string RecordTypeName { get; set; }
    }
}
