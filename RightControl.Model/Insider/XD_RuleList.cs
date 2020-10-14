using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DapperExtensions;

namespace RightControl.Model.Insider
{
    public class XD_RuleList
    {
        [DapperExtensions.Key(true)]
        [Display(Name = "规则编号")]
        public int R_No { get; set; }

        [Display(Name = "规则名称")]
        public string R_Name { get; set; }

        [Display(Name = "规则描述")]
        public string R_Description { get; set; }

        [Display(Name = "模块编号")]
        public int M_No { get; set; }

        [Display(Name = "模块名称")]
        public string M_Name { get; set; }

        [Display(Name = "添加时间")]
        public DateTime CreateOn { get; set; }

    }
}
