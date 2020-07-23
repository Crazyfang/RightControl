using System;
using System.Collections.Generic;
using DapperExtensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RightControl.Model.RecordTrancation
{
    [Table("RecordFileTypeTable")]
    public class RecordFileType
    {
        [DapperExtensions.Key(true)]
        [Display(Name = "主键")]
        public int ID { get; set; }

        [Display(Name = "档案文件类型字符")]
        public string RecordFileTypeString { get; set; }

        [Display(Name = "档案文件类型名称")]
        public string RecordTypeName { get; set; }
    }
}
