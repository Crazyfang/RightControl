using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.RecordTrancation;

namespace RightControl.IService.RecordTrancation
{
    public interface IApplyBorrowService:IBaseService<ApplyBorrowTable>
    {
        bool ApplyBorrow(string borrowId, ApplyBorrowTable obj, List<string> fileList, string operateName);

        bool ApplyBatchBorrow(string borrowId, ApplyBorrowTable obj, string departmentId, string operateName);

        bool ApplyBorrowAgree(string borrowId);
    }
}
