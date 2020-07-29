using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RightControl.Model.Insider
{
    public class YG_InsiderFaRenList
    {
        [DapperExtensions.Key(true)]
        public int Id { get; set; }

        [Display(Name = "法人或其他组织名称")]
        public string FaRenNm { get; set; }

        [Display(Name = "关联类型")]
        public string ReKind { get; set; }

        [Display(Name = "报告单位")]
        public string ReportUnit { get; set; }

        [Display(Name = "与报告单位关系")]
        public string Relationship { get; set; }

        [Display(Name = "说明")]
        public string Description { get; set; }

        [Display(Name = "添加时间")]
        public DateTime CreateOn { get; set; }
    }
}
