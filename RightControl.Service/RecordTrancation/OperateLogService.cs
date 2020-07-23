using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class OperateLogService:BaseService<OperateLog>, IOperateLogService
    {
        public dynamic GetListByFilter(OperateLog filter, PageInfo pageInfo)
        {
            var where = " where 1=1";

            if(!string.IsNullOrEmpty(filter.OperateType))
            {
                where += " and OperateType=@OperateType";
            }
            if(!string.IsNullOrEmpty(filter.RecordId))
            {
                where += " and RecordId=@RecordId";
            }

            return GetListByFilter(filter, pageInfo, where);
        }
    }
}
