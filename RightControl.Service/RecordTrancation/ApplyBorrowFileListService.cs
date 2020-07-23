using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class ApplyBorrowFileListService:BaseService<ApplyBorrowFileList>, IApplyBorrowFileListService
    {
        public dynamic GetListByFilter(ApplyBorrowFileList filter, PageInfo pageInfo)
        {
            string _where = " where 1=1";
            if (string.IsNullOrEmpty(filter.RecordID))
            {
                _where += " and RecordID=@RecordID";
            }

            return GetListByFilter(filter, pageInfo, _where);
        }
    }
}
