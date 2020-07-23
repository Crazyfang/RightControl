using RightControl.IService.CreditCompany;
using RightControl.Model;
using RightControl.Model.CreditSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Service.CreditCompany
{
    public class CreditCompanyService:ICreditCompanyService
    {
        private readonly IFreeSql fsql;
        public CreditCompanyService(IFreeSql freeSql)
        {
            fsql = freeSql;
        }

        public dynamic GetCreditCompanyList(CreditCompanyModel creditCompany, PageInfo pageInfo, string userName)
        {
            var user = fsql.Select<UserModel>().Where(i => i.UserName == userName).First();

            var result = fsql.Select<CreditCompanyModel>()
                //.WhereIf(user != null, i => i.BelongStreet == user.BelongStreet)
                .WhereIf(!string.IsNullOrEmpty(creditCompany.TaxpayerName), i => i.TaxpayerName.Contains(creditCompany.TaxpayerName))
                .WhereIf(!string.IsNullOrEmpty(creditCompany.TaxpayerIdentifier), i => i.TaxpayerIdentifier.Contains(creditCompany.TaxpayerIdentifier))
                .WhereIf(creditCompany.Status != 0, i => i.Status == creditCompany.Status)
                .Count(out var total)
                .Page(pageInfo.page, pageInfo.limit)
                .ToList();

            return new { code = 0, count = total, data = result };
        }
    }
}
