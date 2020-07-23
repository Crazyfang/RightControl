using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.RecordTrancation
{
    public class ApplyCopyFileList
    {
        [DapperExtensions.Key(true)]
        public int ID { get; set; }

        public string BorrowID { get; set; }

        public string RecordID { get; set; }
    }
}
