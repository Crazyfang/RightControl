using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DapperExtensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace RightControl.Model.RecordTrancation
{
    [FreeSql.DataAnnotations.Table(Name = "RecordTable")]
    [DapperExtensions.Table("RecordTable")]
    public class Record
    {
        [DapperExtensions.Key(false)]
        [FreeSql.DataAnnotations.Column(IsIdentity = true, IsPrimary = true)]
        [Display(Name = "档案编号")]
        public string RecordID { get; set; }

        [Display(Name = "档案用户姓名")]
        public string RecordUserName { get; set; }

        [Display(Name = "客户内码")]
        public string RecordUserInCode { get; set; }

        [Display(Name = "档案用户客户码")]
        public string RecordUserCode { get; set; }

        [Display(Name = "档案归属支行")]
        public string RecordManagerDepartment { get; set; }

        [Display(Name = "档案状态")]
        public int RecordStatus { get; set; }

        [Display(Name = "授信到期日")]
        public DateTime CreditDueDate { get; set; }

        [Display(Name = "档案归属客户经理")]
        public string RecordManager { get; set; }

        [Display(Name = "档案存储位置")]
        public string RecordStorageLoc { get; set; }

        [Display(Name = "档案类型")]
        public int RecordType { get; set; }

        [Display(Name = "档案创建时间")]
        public DateTime CreateOn { get; set; }

        [Display(Name = "档案移交时间")]
        public DateTime? HandOverTime { get; set; }

        [Computed]
        [FreeSql.DataAnnotations.Column(IsIgnore = true)]
        [Display(Name = "档案归属支行")]
        public string RecordDepartmentName { get; set; }

        [Computed]
        [FreeSql.DataAnnotations.Column(IsIgnore = true)]
        [Display(Name = "档案归属客户经理")]
        public string RecordManagerName { get; set; }
    }
}
