using RightControl.Common;
using RightControl.IService.CreditCompany;
using RightControl.Model;
using RightControl.Model.CreditSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace RightControl.Service.CreditCompany
{
    public class CreditCompanyService : ICreditCompanyService
    {
        private readonly IFreeSql fsql;
        public CreditCompanyService(IFreeSql freeSql)
        {
            fsql = freeSql;
        }

        public dynamic GetCreditCompanyList(CreditCompanyModel creditCompany, PageInfo pageInfo, string userName)
        {
            var user = fsql.Select<UserModel>()
                .Where(i => i.UserName == userName)
                .IncludeMany(i => i.DepartmentList)
                .First();
            var department = user.DepartmentList.FirstOrDefault();

            if(string.IsNullOrEmpty(department.BelongStreet))
            {
                var result = fsql.Select<CreditCompanyModel>()
                    .WhereIf(!string.IsNullOrEmpty(creditCompany.TaxpayerName), i => i.TaxpayerName == creditCompany.TaxpayerName)
                    .WhereIf(!string.IsNullOrEmpty(creditCompany.TaxpayerIdentifier), i => i.TaxpayerIdentifier == creditCompany.TaxpayerIdentifier)
                    .WhereIf(creditCompany.Status != 0, i => i.Status == creditCompany.Status)
                    .Count(out var total)
                    .Page(pageInfo.page, pageInfo.limit)
                    .ToList();

                return new { code = 0, count = total, data = result };
            }

            //精细查询情况
            if (!string.IsNullOrEmpty(creditCompany.TaxpayerName) || !string.IsNullOrEmpty(creditCompany.TaxpayerIdentifier))
            {
                var result = fsql.Select<CreditCompanyModel>()
                    //.WhereIf(user != null, i => i.BelongStreet == user.BelongStreet)
                    .WhereIf(!string.IsNullOrEmpty(creditCompany.TaxpayerName), i => i.TaxpayerName == creditCompany.TaxpayerName)
                    .WhereIf(!string.IsNullOrEmpty(creditCompany.TaxpayerIdentifier), i => i.TaxpayerIdentifier == creditCompany.TaxpayerIdentifier)
                    .WhereIf(creditCompany.Status != 0, i => i.Status == creditCompany.Status)
                    .Count(out var total)
                    .Page(pageInfo.page, pageInfo.limit)
                    .ToList();

                return new { code = 0, count = total, data = result };
            }
            //不是精细查询情况
            else
            {
                var claimCount = fsql.Select<ClaimFormModel>()
                    .Where(i => i.UserId == user.Id)
                    .Where(i => i.CreditCompany.Status == 2 || i.CreditCompany.Status == 3)
                    .Where(i => i.EndSign == 0)
                    .Count();
                //认领企业超过10个情况
                if (claimCount >= 10)
                {
                    return new { code = 0, count = 0, data = new List<CreditCompanyModel>() };
                }
                else
                {
                    var count = fsql.Select<CreditCompanyModel>()
                        .WhereIf(department.Id != 0, i => i.BelongStreet == department.BelongStreet)
                        .Where(i => i.Status == 1)
                        .Count();

                    if (count > 1)
                    {
                        var seed = count > 10 ? new Random().Next(1, count.ToInt() / 10) : 1;
                        var result = fsql.Select<CreditCompanyModel>()
                            .Where(i => i.Status == 1 && i.BelongStreet == department.BelongStreet)
                            .Page(seed, 10)
                            .ToList();

                        return new { code = 0, count = 10, data = result };
                    }
                    else
                    {
                        return new { code = 0, count = 0, data = new List<CreditCompanyModel>() };
                    }

                }
            }
        }
    }
}
