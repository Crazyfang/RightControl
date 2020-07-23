using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;

namespace RightControl.Model.InforSearch
{
    public class BlackList
    {
        [DapperExtensions.Key(true)]
        public int Id { get; set; }

        [Display(Name = "银行名称")]
        public string BankName { get; set; }

        [Display(Name = "拉黑原因")]
        public string RefuseReason { get; set; }

        [Display(Name = "拉黑时间")]
        public DateTime CreateOn { get; set; }

        [Display(Name = "编辑人ID")]
        public int EditorId { get; set; }

        [Computed]
        [Display(Name = "编辑人姓名")]
        public string EditorName { get; set; }

        [Display(Name = "其他说明")]
        public string OtherDescription { get; set; }

        [Computed]
        [Display(Name = "查询人")]
        public string SearchMan { get; set; }

        [Computed]
        [Display(Name = "查询支行")]
        public string SearchBranch { get; set; }

        [Computed]
        [Display(Name = "查询时间")]
        public DateTime SearchTime { get; set; }
    }
}
