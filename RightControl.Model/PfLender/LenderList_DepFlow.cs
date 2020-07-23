using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RightControl.Model.PfLender
{
    public class LenderList_DepFlow
    {
        [Display(Name = "交易日期")]
        public string BJ01DATE { get; set; }

        [Display(Name = "渠道类别")]
        public string BJ02QDLB { get; set; }

        [Display(Name = "账号")]
        public string BJ03AC15 { get; set; }

        [Display(Name = "币种")]
        public string BJ04CCYC { get; set; }

        [Display(Name = "钞汇属性")]
        public string BJ05CHSX { get; set; }

        [Display(Name = "原交易流水号")]
        public string BJ06TXSN { get; set; }

        [Display(Name = "前置流水号")]
        public string BJ10SN12 { get; set; }

        [Display(Name = "交易机构")]
        public string BJ33BRNO { get; set; }

        [Display(Name = "交易柜员")]
        public string BJ34STAF { get; set; }

        [Display(Name = "交易卡账户归属机构")]
        public string BJ35BRNO { get; set; }

        [Display(Name = "交易卡账号")]
        public string BJ11AC22 { get; set; }

        [Display(Name = "实际交易金额")]
        public double BJ16AMT { get; set; }

        [Display(Name = "借贷标志")]
        public string BJ17CDFG { get; set; }

        [Display(Name = "对方账号")]
        public string BJ18AC32 { get; set; }

        [Display(Name = "交易类型")]
        public string BJ19JYLX { get; set; }

        [Display(Name = "现转标志")]
        public string BJ20FLAG { get; set; }

        [Display(Name = "卡号")]
        public string CARD_NO { get; set; }

        [Display(Name = "户名")]
        public string ACCOUNT_NM { get; set; }

        [Display(Name = "客户内码")]
        public string CUST_CSNO { get; set; }

        [Display(Name = "客户号")]
        public string CUST_ID { get; set; }

        [Display(Name = "证件号")]
        public string CRET_ID { get; set; }
    }
}
