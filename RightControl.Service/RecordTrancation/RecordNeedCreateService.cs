using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class RecordNeedCreateService:BaseService<RecordNeedCreate>,IRecordNeedCreateService
    {
        public dynamic GetListByFilter(RecordNeedCreate filter, PageInfo pageInfo)
        {
            pageInfo.field = "ContractNo";
            pageInfo.order = "asc";
            string where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.ContractNo))
            {
                where += " and ContractNo=@ContractNo";
            }

            return GetListByFilter(filter, pageInfo, where);
        }
    }
}
