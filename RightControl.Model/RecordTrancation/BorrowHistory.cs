using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.RecordTrancation
{
    [Table("BorrowHistory")]
    public class BorrowHistory
    {
        [DapperExtensions.Key(false)]
        public string BorrowID { get; set; }

        public string BorrowUser { get; set; }

        public DateTime BorrowTime { get; set; }

        public DateTime ReturnTime { get; set; }
    }
}
