using RightControl.Common;
using RightControl.Model.CreditSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.IService.CreditCompany
{
    public interface IClaimFormService
    {
        AjaxResult ClaimCreditCompany(int creditCompanyId, string userName);

        List<ClaimFormModel> ClaimHistory(int creditCompanyId);

        List<CreditCompanyModel> GetTaxpayerList(string userName);

        bool VisitingFeedback(VisitingConfirm obj);
    }
}
