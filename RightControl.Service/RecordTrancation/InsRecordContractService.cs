using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class InsRecordContractService:BaseService<InsRecordContract>, IInsRecordContractService
    {
        public dynamic GetListByFilter(InsRecordContract filter, PageInfo pageInfo)
        {
            var where = " where 1=1 and DeleteSign=0";

            return GetListByFilter(filter, pageInfo, where);
        }
    }
}
