using DapperExtensions;
using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace RightControl.Model.RecordTrancation
{
    [FreeSql.DataAnnotations.Table(Name = "RecordList")]
    [DapperExtensions.Table("RecordList")]
    public class RecordList
    {
        [DapperExtensions.Key(true)]
        [FreeSql.DataAnnotations.Column(IsIdentity = true, IsPrimary = true)]
        [Display(Name = "无意义自增主键")]
        public int ID { get; set; }

        [Display(Name = "档案编号")]
        public string RecordId { get; set; }

        [Display(Name = "数量")]
        public int Amount { get; set; }

        [Display(Name = "文件ID")]
        public int FileID { get; set; }

        [Computed]
        [Navigate(nameof(FileID))]
        public FileList File { get; set; }

        [Display(Name = "排序号")]
        public int FileFilterNum { get; set; }

        [Display(Name = "到期时间")]
        public DateTime? ExpirationTime { get; set; }

        [Computed]
        [FreeSql.DataAnnotations.Column(IsIgnore = true)]
        public string RecordFileTypeString { get; set; }

        [Computed]
        [FreeSql.DataAnnotations.Column(IsIgnore = true)]
        public string RecordFileName { get; set; }

        public int RecordType { get; set; }

        [Computed]
        [FreeSql.DataAnnotations.Column(IsIgnore = true)]
        public string HoldingCell { get; set; }

        [Computed]
        [FreeSql.DataAnnotations.Column(IsIgnore = true)]
        public string OriginalRecordType { get; set; }

        [Computed]
        public ExpiredFileVerifyEntity ExpiredFileVerifyEntity { get; set; }
    }
}
