using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RightControl.Model.Insider
{
    public class YG_InsiderRltsList
    {
        [DapperExtensions.Key(true)]
        public int Id { get; set; }

        [Display(Name = "岗位")]
        public string InsiderPs { get; set; }

        [Display(Name = "内部人姓名")]
        public string InsiderNm { get; set; }

        [Display(Name = "姓名")]
        public string RltsNm { get; set; }

        [Display(Name = "身份证号码")]
        public string IdentityCd { get; set; }

        [Display(Name = "与内部人关系")]
        public string Relationship { get; set; }

        [Display(Name = "所控制企业名称")]
        public string CompanyNm { get; set; }

        [Display(Name = "统一社会信用代码证号")]
        public string SocialCreditCode { get; set; }

        [Display(Name = "控制人")]
        public string Controller { get; set; }

        [Display(Name = "与内部人关系")]
        public string ControllerRlts { get; set; }

        [Display(Name = "添加时间")]
        public DateTime CreateOn { get; set; }
    }
}
