using System.Collections.Generic;
using RightControl.Model.RecordTrancation;

namespace RightControl.IRepository.RecordTrancation
{
    public interface IContractFileTypeRepository:IBaseRepository<ContractFileType>
    {
        List<ContractFileType> GetListByRecordId(string recordId);
    }
}
