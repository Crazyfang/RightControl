using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DapperExtensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.RecordTrancation
{
    [FreeSql.DataAnnotations.Table(Name = "FileList")]
    [Table("FileList")]
    public class FileList
    {
        [DapperExtensions.Key(true)]
        [FreeSql.DataAnnotations.Column(IsIdentity = true, IsPrimary = true)]
        [Display(Name = "文件ID")]
        public int FileID { get; set; }

        [Display(Name = "文件名称")]
        public string FileName { get; set; }

        [Display(Name = "启用标志")]
        public bool ActiveMark { get; set; }

        [Display(Name = "档案文件类型")]
        public int RecordFileType { get; set; }
    }
}
