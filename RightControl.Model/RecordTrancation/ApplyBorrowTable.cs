using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.RecordTrancation
{
    [Table("ApplyBorrowTable")]
    public class ApplyBorrowTable
    {
        [DapperExtensions.Key(false)]
        public string BorrowID { get; set; }

        public DateTime ApplyTime { get; set; }

        public string ApplyUser { get; set; }

        public int ApplyState { get; set; }
    }
}
