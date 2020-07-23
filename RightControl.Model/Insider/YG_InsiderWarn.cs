using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DapperExtensions;

namespace RightControl.Model.Insider
{
    public class YG_InsiderWarn
    {
        [Display(Name = "数据时间")]
        public string DataDate { get; set; }

        [Display(Name = "规则编号")]
        public string R_No { get; set; }

        [Display(Name = "规则名称")]
        public string R_Name { get; set; }

        [Display(Name = "关联方")]
        public string Insider { get; set; }

        [Display(Name = "预警信息")]
        public string WarnInfo { get; set; }

        [Computed]
        [Display(Name = "查询时间")]
        public string SearchTime { get; set; }
    }
}
