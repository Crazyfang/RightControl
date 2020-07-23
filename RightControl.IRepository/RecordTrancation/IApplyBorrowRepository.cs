using RightControl.Model.RecordTrancation;
using System.Collections.Generic;

namespace RightControl.IRepository.RecordTrancation
{
    public interface IApplyBorrowRepository:IBaseRepository<ApplyBorrowTable>
    {
        //申请借阅档案
        bool BorrowRecord(string borrowId, ApplyBorrowTable obj, List<string> fileList, string operateName);

        //批量申请借阅档案
        bool ApplyBatchBorrow(string borrowId, ApplyBorrowTable obj, string departmentId, string operateName);

        //同意借阅
        bool ApplyBorrowAgree(string borrowId);
    }
}
