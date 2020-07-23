using RightControl.Model;
using RightControl.Model.CreditSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.IService.CreditCompany
{
    public interface ICreditCompanyService
    {
        dynamic GetCreditCompanyList(CreditCompanyModel creditCompany, PageInfo pageInfo, string userName);
    }
}
