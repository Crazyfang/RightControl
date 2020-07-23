using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class ApplyCopyFileListService:BaseService<ApplyCopyFileList>, IApplyCopyFileListService
    {
        public dynamic GetListByFilter(ApplyCopyFileList filter, PageInfo pageInfo)
        {
            string where = " where 1=1";
            if (string.IsNullOrEmpty(filter.RecordID))
            {
                where += " and RecordID=@RecordID";
            }

            return GetListByFilter(filter, pageInfo, where);
        }
    }
}
