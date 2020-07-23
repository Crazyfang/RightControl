using System;

namespace RightControl.Model.RecordTrancation
{
    public class ApplyCopyTable
    {
        [DapperExtensions.Key(false)]
        public string BorrowID { get; set; }

        public DateTime ApplyTime { get; set; }

        public string ApplyUser { get; set; }

        public int ApplyState { get; set; }
    }
}
