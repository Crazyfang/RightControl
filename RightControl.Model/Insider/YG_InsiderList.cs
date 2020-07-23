using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RightControl.Model.Insider
{
    public class YG_InsiderList
    {
        [DapperExtensions.Key(true)]
        public int Id { get; set; }

        [Display(Name = "关联方名称")]
        public string RepartyNm { get; set; }

        [Display(Name = "关联性质")]
        public string ReNature { get; set; }

        [Display(Name = "所在支行/部门")]
        public string Department { get; set; }

        [Display(Name = "工作岗位")]
        public string Post { get; set; }

        [Display(Name = "身份证号码")]
        public string IdentityCd { get; set; }

        [Display(Name = "添加时间")]
        public DateTime CreateOn { get; set; }
    }
}
