using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.RecordTrancation
{
    public class ChangedRecordManager
    {
        [DapperExtensions.Key(true)]
        public int ID { get; set; }

        public string CustINNO { get; set; }

        public string RecordID { get; set; }

        public string RecordUserName { get; set; }
    }
}
