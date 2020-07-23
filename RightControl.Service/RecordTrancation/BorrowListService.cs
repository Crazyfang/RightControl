using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class BorrowListService:BaseService<BorrowList>, IBorrowListService
    {
        public dynamic GetListByFilter(BorrowList filter, PageInfo pageInfo)
        {
            var _where = " where 1=1";

            if (!string.IsNullOrEmpty(filter.BorrowID))
            {
                _where += " and BorrowID=@BorrowID";
            }

            return GetListByFilter(filter, pageInfo, _where);
        }
    }
}
