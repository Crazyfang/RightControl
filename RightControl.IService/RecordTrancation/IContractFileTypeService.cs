using System.Collections.Generic;
using RightControl.Model.RecordTrancation;

namespace RightControl.IService.RecordTrancation
{
    public interface IContractFileTypeService:IBaseService<ContractFileType>
    {
        int Create(ContractFileType model);

        List<ContractFileType> GetListByRecordId(string recordId);
    }
}
