using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;

namespace RightControl.Model.RecordTrancation
{
    public class SelectType
    {
        [DapperExtensions.Key(true)]
        public int Id { get; set; }

        [Display(Name ="文件类别")]
        public int FileType { get; set; }

        [Display(Name = "选择类别")]
        public int SelectTypeNum { get; set; }

        [Computed]
        public string RecordFileTypeName { get; set; }
    }
}
