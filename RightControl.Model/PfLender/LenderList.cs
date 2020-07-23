using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using System.ComponentModel.DataAnnotations;

namespace RightControl.Model.PfLender
{
    public class LenderList
    {
        [DapperExtensions.Key(true)]
        public int Id { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "身份证")]
        public string IdentityCd { get; set; }

        [Display(Name = "户籍地")]
        public string Place { get; set; }

        [Display(Name = "近三年案件数量")]
        public int LawCase { get; set; }

        [Display(Name = "标的额(万元)")]
        public double Amount { get; set; }

        [Display(Name = "添加时间")]
        public DateTime CreateOn { get; set; }

    }
}
