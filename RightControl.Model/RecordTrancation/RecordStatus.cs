using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DapperExtensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.RecordTrancation
{
    [Table("RecordStatus")]
    public class RecordStatus
    {
        [DapperExtensions.Key(true)]
        [Display(Name = "主键")]
        public int ID { get; set; }

        [Display(Name = "档案状态ID")]
        public int StatusID { get; set; }

        [Display(Name = "档案状态名称")]
        public string StatusName { get; set; }
    }
}
