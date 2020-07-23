using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.RecordTrancation;

namespace RightControl.IRepository.RecordTrancation
{
    public interface ILoanManagerRepository:IBaseRepository<LoanManager>
    {
        List<LoanManager> GetLoanManagersByCustInCode(string custInCode);
    }
}
