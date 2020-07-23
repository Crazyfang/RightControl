using System;
using System.Data;
using RightControl.IRepository.RecordTrancation;
using RightControl.Model.RecordTrancation;
using DapperExtensions.SqlServerExt;

namespace RightControl.Repository.RecordTrancation
{
    public class BorrowHistoryRepository:BaseRepository<BorrowHistory>, IBorrowHistoryRepository
    {
        public bool ReturnBorrowFile(string borrowId)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                     var applyBorrow = conn.GetById<ApplyBorrowTable>(borrowId, null, transaction);

                    var borrowHistory = new BorrowHistory()
                    {
                        BorrowID = applyBorrow.BorrowID,
                        BorrowUser = applyBorrow.ApplyUser,
                        BorrowTime = applyBorrow.ApplyTime,
                        ReturnTime = DateTime.Now
                    };
                    //插入借阅历史记录表
                    conn.Insert(borrowHistory, transaction);
                    //删除原有申请借阅记录
                    conn.DeleteById<ApplyBorrowTable>(borrowId, transaction);

                    var applyBorrowFileList = conn.GetByWhere<ApplyBorrowFileList>(" where BorrowID=@BorrowID",
                        new {BorrowID = borrowId}, null, null, transaction);

                    //循环插入借阅历史文件表
                    foreach (var temp in applyBorrowFileList)
                    {
                        var borrowList = new BorrowList()
                        {
                            BorrowID = temp.BorrowID,
                            RecordID = temp.RecordID
                        };

                        conn.Insert<BorrowList>(borrowList, transaction);
                        conn.DeleteById<ApplyBorrowFileList>(temp.ID, transaction);
                        //更新档案状态
                        conn.UpdateByWhere<Record>(" where RecordID=@RecordID", "RecordStatus",
                            new {RecordID = temp.RecordID, RecordStatus = 2}, transaction);
                    }

                    transaction.Commit();
                    return true;
                }
                catch (System.Exception e)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
