using RightControl.Model.RecordTrancation;
using System.Collections.Generic;

namespace RightControl.IRepository.RecordTrancation
{
    public interface IApplyCopyRepository:IBaseRepository<ApplyCopyTable>
    {
        bool CopyRecord(string borrowId, ApplyCopyTable obj, List<string> fileList);
    }
}
