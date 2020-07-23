using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RightControl.Model.PfLender
{
    public class LenderList_Dep
    {
        [Display(Name = "数据时间")]
        public string DATA_DATE { get; set; }

        [Display(Name = "存款账号")]
        public string ACCOUNT_NO { get; set; }

        [Display(Name = "客户内码")]
        public string CUST_CSNO { get; set; }

        [Display(Name = "客户号")]
        public string CUST_ID { get; set; }

        [Display(Name = "证件号")]
        public string CRDT_ID { get; set; }

        [Display(Name = "客户姓名")]
        public string CUST_NM { get; set; }

        [Display(Name = "机构号")]
        public string ORG_NO { get; set; }

        [Display(Name = "存款类型")]
        public string DEP_TYPE { get; set; }

        [Display(Name = "保证金类型")]
        public string MARGIN_TYPE { get; set; }

        [Display(Name = "日切余额")]
        public double CURR_VAL { get; set; }

        [Display(Name = "年日均")]
        public double YEAR_AVG { get; set; }

        [Display(Name = "年积数")]
        public double TOTAL_VAL { get; set; }

        [Display(Name = "月日均")]
        public double MONTH_AVG { get; set; }

        [Display(Name = "季日均")]
        public double QUARTER_AVG { get; set; }

        [Display(Name = "同期日均")]
        public double SM_PRD_AVG { get; set; }

        [Display(Name = "存期(定期)")]
        public double DEP_PERIOD { get; set; }

        [Display(Name = "到期日期(定期)")]
        public string EXPIRE_DT { get; set; }

        [Display(Name = "产品名称(定期)")]
        public string PRODUCT_NM { get; set; }

        [Display(Name = "利率(定期)")]
        public decimal RATE { get; set; }
    }
}
