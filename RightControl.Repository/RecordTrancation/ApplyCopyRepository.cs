using RightControl.IRepository.RecordTrancation;
using RightControl.Model.RecordTrancation;
using System.Collections.Generic;
using System.Data;
using DapperExtensions.SqlServerExt;
using RightControl.Common;

namespace RightControl.Repository.RecordTrancation
{
    public class ApplyCopyRepository:BaseRepository<ApplyCopyTable>, IApplyCopyRepository
    {
        public bool CopyRecord(string borrowId, ApplyCopyTable obj, List<string> fileList)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    //循环插入借阅档案
                    foreach (var item in fileList)
                    {
                        //var operate = new OperateLog()
                        //{
                        //    OperateTime = DateTime.Now,
                        //    OperateType = "申请借阅",
                        //    RecordId = item,
                        //    OperatePeople = operateName,
                        //    OperateInfo = $"申请借阅人:{obj.ApplyUser},借阅号:{borrowId},借阅档案:{item}"
                        //};

                        //conn.Insert<OperateLog>(operate, transaction);

                        var data = new ApplyCopyFileList()
                        {
                            BorrowID = borrowId,
                            RecordID = item
                        };

                        conn.Insert(data, transaction);

                        //更改档案状态
                        //var tempModel = conn.GetById<Record>(item, null, transaction);
                        //tempModel.RecordStatus = 3;
                        //conn.UpdateById<Record>(tempModel, "RecordStatus", transaction);
                    }

                    conn.Insert(obj, transaction);

                    //事务提交
                    transaction.Commit();
                    return true;
                }
                catch (System.Exception e)
                {
                    Log.WriteLog(e);
                    //发生错误回滚
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
