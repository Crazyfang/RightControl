using System;
using System.Collections.Generic;
using DapperExtensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.RecordTrancation
{
    [Table("ApplyBorrowFileList")]
    public class ApplyBorrowFileList
    {
        [Key(true)]
        public int ID { get; set; }

        public string BorrowID { get; set; }

        public string RecordID { get; set; }
    }
}
