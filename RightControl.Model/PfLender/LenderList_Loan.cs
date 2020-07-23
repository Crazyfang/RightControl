using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RightControl.Model.PfLender
{
    public class LenderList_Loan
    {
        [Display(Name = "数据时间")]
        public string DATA_DATE { get; set; }

        [Display(Name = "机构号")]
        public string ORG_NO { get; set; }

        [Display(Name = "机构名称")]
        public string ORG_NM { get; set; }

        [Display(Name = "贷款账号")]
        public string ACCOUNT_NO { get; set; }

        [Display(Name = "贷款科目号")]
        public string ACCOUNT_ACID { get; set; }

        [Display(Name = "贷款科目名称")]
        public string ACCOUNT_ACID_NM { get; set; }

        [Display(Name = "产品代码")]
        public string PRD_COD { get; set; }

        [Display(Name = "产品名称")]
        public string PRD_NM { get; set; }

        [Display(Name = "贷款用途")]
        public string LOAN_USE { get; set; }

        [Display(Name = "客户号")]
        public string CUST_ID { get; set; }

        [Display(Name = "客户内码")]
        public string CUST_CSNO { get; set; }

        [Display(Name = "客户名称")]
        public string CUST_NM { get; set; }

        [Display(Name = "客户联系地址")]
        public string CUST_ADDR { get; set; }

        [Display(Name = "贷款合同号")]
        public string CONTRACT_NO { get; set; }

        [Display(Name = "借据序号")]
        public double IOU_NUM { get; set; }

        [Display(Name = "担保方式")]
        public string GUA_TYPE { get; set; }

        [Display(Name = "贷款期限")]
        public string LOAN_TERM { get; set; }

        [Display(Name = "贷款合同金额")]
        public double CONTRACT_VAL { get; set; }

        [Display(Name = "贷款余额")]
        public double CURR_VAL { get; set; }

        [Display(Name = "行业分类")]
        public string CLASSIFY { get; set; }

        [Display(Name = "发放金额")]
        public double PAYOUT_VAL { get; set; }

        [Display(Name = "贷款发放日期")]
        public string PAYOUT_DT { get; set; }

        [Display(Name = "贷款到期日期")]
        public string MATRT_DT { get; set; }

        [Display(Name = "经办客户经理")]
        public string LN_TLR { get; set; }

        [Display(Name = "贷款类型")]
        public string LOAN_TYPE { get; set; }

        [Display(Name = "授信额度")]
        public double CRE_QUOTA2 { get; set; }
    }
}
