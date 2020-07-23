using RightControl.IRepository.RecordTrancation;
using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class BorrowHistoryService:BaseService<BorrowHistory>, IBorrowHistoryService
    {
        public IBorrowHistoryRepository BorrowHistoryRepository { get; set; }
        public dynamic GetListByFilter(BorrowHistory filter, PageInfo pageInfo)
        {
            var _where = " where 1=1";

            if (!string.IsNullOrEmpty(filter.BorrowUser))
            {
                _where += " and BorrowUser=@BorrowUser";
            }

            return GetListByFilter(filter, pageInfo, _where);
        }

        public bool ReturnBorrowFile(string borrowId)
        {
            return BorrowHistoryRepository.ReturnBorrowFile(borrowId);
        }
    }
}
