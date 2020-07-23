using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RightControl.Model.Insider
{
    public class Parameter
    {
        [DapperExtensions.Key(true)]
        public int P_No { get; set; }

        [Display(Name = "参数名称")]
        public string P_Name { get; set; }

        [Display(Name = "参数类型")]
        public string P_Type { get; set; }

        [Display(Name = "参数最大值")]
        public string P_Max { get; set; }

        [Display(Name = "参数最小值")]
        public string P_Min { get; set; }

        [Display(Name = "添加时间")]
        public DateTime CreateOn { get; set; }

        [Display(Name = "规则编号")]
        public int R_No { get; set; }

        [Display(Name = "规则名称")]
        public string R_Name { get; set; }

        [Display(Name = "模块编号")]
        public int M_No { get; set; }

        [Display(Name = "模块名称")]
        public string M_Name { get; set; }

    }
}
