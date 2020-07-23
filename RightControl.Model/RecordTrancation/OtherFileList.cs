using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DapperExtensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.RecordTrancation
{
    [FreeSql.DataAnnotations.Table(Name = "OtherFileListTable")]
    [Table("OtherFileListTable")]
    public class OtherFileList
    {
        [DapperExtensions.Key(true)]
        [FreeSql.DataAnnotations.Column(IsIdentity = true, IsPrimary = true)]
        [Display(Name = "主键")]
        public int ID { get; set; }

        [Display(Name = "档案编号")]
        public string RecordID { get; set; }

        [Display(Name = "档案文件类型")]
        public int RecordFileType { get; set; }

        [Display(Name = "数量")]
        public int Amount { get; set; }

        [Display(Name = "文件名称")]
        public string FileName { get; set; }

        [Display(Name = "过期时间")]
        public DateTime? ExpirationTime { get; set; }

        [Computed]
        [FreeSql.DataAnnotations.Column(IsIgnore = true)]
        public string RecordFileTypeString { get; set; }

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
