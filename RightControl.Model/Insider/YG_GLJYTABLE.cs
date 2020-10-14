using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RightControl.Model.Insider
{
    public class YG_GLJYTABLE
    {

        [Display(Name = "数据时间")]
        public string DATA_DATE { get; set; }

        [Display(Name = "关联方名称")]
        public string INSIDERNM { get; set; }

        [Display(Name = "证件类型")]
        public string CERTTYPE { get; set; }

        [Display(Name = "证件代码")]
        public string CERT_NO { get; set; }

        [Display(Name = "授信总额")]
        public string SHOUXIN { get; set; }

        [Display(Name = "贷款余额")]
        public string LOAN { get; set; }

        [Display(Name = "信用贷款")]
        public string XINYONG_LOAN { get; set; }

        [Display(Name = "贷记卡")]
        public string CRET_CARD { get; set; }

        [Display(Name = "互联网金融贷款余额")]
        public string INTER_FINANCE { get; set; }

        [Display(Name = "票据承兑")]
        public string BILL_ACCEPT { get; set; }

        [Display(Name = "票据贴现")]
        public string BILL_DISCOUNTED { get; set; }

        [Display(Name = "担保")]
        public string DANBAO { get; set; }

        [Display(Name = "保证金、质押的银行存单和国债余额")]
        public string ZHIYA { get; set; }

        [Display(Name = "授信余额")]
        public string GRANT_BALANCE { get; set; }
    }
}
