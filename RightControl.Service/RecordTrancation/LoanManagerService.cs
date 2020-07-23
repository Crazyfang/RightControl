using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;
using System.Collections.Generic;
using RightControl.IRepository.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class LoanManagerService:BaseService<LoanManager>, ILoanManagerService
    {
        public ILoanManagerRepository LoanManagerRepository { get; set; }
        public dynamic GetListByFilter(LoanManager filter, PageInfo pageInfo)
        {
            var where = " where 1=1";

            return GetListByFilter(filter, pageInfo, where);
        }

        public List<LoanManager> GetLoanManagersByCustInCode(string custInCode)
        {
            return LoanManagerRepository.GetLoanManagersByCustInCode(custInCode);
        }
    }
}
