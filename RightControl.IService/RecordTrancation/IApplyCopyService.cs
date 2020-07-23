using RightControl.Model.RecordTrancation;
using System.Collections.Generic;

namespace RightControl.IService.RecordTrancation
{
    public interface IApplyCopyService:IBaseService<ApplyCopyTable>
    {
        bool CopyRecord(string borrowId, ApplyCopyTable obj, List<string> fileList);
    }
}
