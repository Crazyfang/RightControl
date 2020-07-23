using RightControl.Model.RecordTrancation;

namespace RightControl.IRepository.RecordTrancation
{
    public interface IBorrowHistoryRepository:IBaseRepository<BorrowHistory>
    {
        bool ReturnBorrowFile(string borrowId);
    }
}
