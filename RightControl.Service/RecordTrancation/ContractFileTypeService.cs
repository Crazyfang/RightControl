using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using RightControl.IRepository.RecordTrancation;
using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class ContractFileTypeService:BaseService<ContractFileType>, IContractFileTypeService
    {
        public IContractFileTypeRepository repository { get; set; }
        public dynamic GetListByFilter(ContractFileType model, PageInfo pageInfo)
        {
            var where = new StringBuilder();
            where.Append(" where 1=1");
            if (!string.IsNullOrEmpty(model.HoldingCell))
            {
                where.Append(" and HoldingCell=@HoldingCell");
            }

            return GetListByFilter(model, pageInfo, where.ToString());
        }

        public int Create(ContractFileType model)
        {
            return repository.Create(model);
        }

        public List<ContractFileType> GetListByRecordId(string recordId)
        {
            return repository.GetListByRecordId(recordId);
        }
    }
}
