using System;
using System.ComponentModel.DataAnnotations;
using DapperExtensions;

namespace RightControl.Model.RecordTrancation
{
    [Table("ZH_XDLZ_LoanContractNeedCreate")]
    public class RecordNeedCreate
    {
        //数据日期
        [DapperExtensions.Key(false)]
        public DateTime Data_date { get; set; }

        [Display(Name = "合同编号")]
        [DapperExtensions.Key(false)]
        public string ContractNo { get; set; }

        [Display(Name = "客户内码")]
        public string CustINNO { get; set; }

        [Display(Name = "客户号")]
        public string CustNO { get; set; }

        [Display(Name = "客户姓名")]
        public string Custname { get; set; }
        
        public string CState { get; set; }
        
        public DateTime Begdate { get; set; }

        public DateTime Enddate { get; set; }

        public double Bal { get; set; }

        public double LX { get; set; }

        public string FKFS { get; set; }

        public string group_idx { get; set; }
    }
}