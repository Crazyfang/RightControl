using RightControl.Model.RecordTrancation;

namespace RightControl.IService.RecordTrancation
{
    public interface IBorrowHistoryService:IBaseService<BorrowHistory>
    {
        bool ReturnBorrowFile(string borrowId);
    }
}
