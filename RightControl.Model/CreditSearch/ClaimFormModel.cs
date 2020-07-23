using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.CreditSearch
{
    /// <summary>
    /// 认领表
    /// </summary>
    [Table(Name = "ClaimForm")]
    public class ClaimFormModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsIdentity =true, IsPrimary = true)]
        public int Id { get; set; }

        public int CreditCompanyId { get; set; }

        public CreditCompanyModel CreditCompany { get; set; }

        public int UserId { get; set; }

        public UserModel User { get; set; }

        /// <summary>
        /// 认领时间
        /// </summary>
        public DateTime ClaimTime { get; set; }

        /// <summary>
        /// 走访时间
        /// </summary>
        public DateTime? VisitingTime { get; set; }

        /// <summary>
        /// 走访反馈
        /// </summary>
        public int VisitingFeedback { get; set; } 

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 持续走访字段
        /// </summary>
        public int ContinueItem { get; set; }

        /// <summary>
        /// 结束标志
        /// </summary>
        public int EndSign { get; set; }
    }
}
