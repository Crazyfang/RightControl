using System;

namespace RightControl.Model.RecordTrancation
{
    [FreeSql.DataAnnotations.Table(Name = "OperateLog")]
    public class OperateLog
    {
        [DapperExtensions.Key(true)]
        [FreeSql.DataAnnotations.Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        public string RecordId { get; set; }

        public string OperateType { get; set; }

        public string OperateInfo { get; set; }

        public string OperatePeople { get; set; }

        public DateTime OperateTime { get; set; }
    }
}
