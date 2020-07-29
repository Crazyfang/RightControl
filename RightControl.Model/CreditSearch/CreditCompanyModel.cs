using FreeSql.DataAnnotations;

namespace RightControl.Model.CreditSearch
{
    [Table(Name = "CreditCompany")]
    public class CreditCompanyModel
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        /// <summary>
        /// 纳税人名称
        /// </summary>
        public string TaxpayerName { get; set; }

        /// <summary>
        /// 纳税人识别号
        /// </summary>
        public string TaxpayerIdentifier { get; set; }

        /// <summary>
        /// 生产经营地址
        /// </summary>
        public string ProductionAddress { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// 法定代表人
        /// </summary>
        public string LegalRepresentative { get; set; }

        /// <summary>
        /// 信用级别
        /// </summary>
        public string CreditRating { get; set; }

        /// <summary>
        /// 所属街道
        /// </summary>
        public string BelongStreet { get; set; }

        /// <summary>
        /// 纳税企业状态 1-待认领 2-已认领 3-持续跟进中 4-完成走访
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否存量客户
        /// </summary>
        public int? IsLoan { get; set; }
    }
}
