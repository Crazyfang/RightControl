using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;

namespace RightControl.Model.RecordSetting
{
    [Table("SelectTypeName")]
    public class SelectTypeName
    {
        [DapperExtensions.Key(true)]
        [Display(Name = "主键")]
        public int Id { get; set; }

        [Display(Name = "档案类别名称")]
        public string SelectTypeNameString { get; set; }

    }
}
