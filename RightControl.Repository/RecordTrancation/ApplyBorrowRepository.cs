using DapperExtensions.SqlServerExt;
using RightControl.Common;
using RightControl.IRepository.RecordTrancation;
using RightControl.Model.RecordTrancation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RightControl.Repository.RecordTrancation
{
    public class ApplyBorrowRepository : BaseRepository<ApplyBorrowTable>, IApplyBorrowRepository
    {
        public bool BorrowRecord(string borrowId, ApplyBorrowTable obj, List<string> fileList, string operateName)
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

                        var data = new ApplyBorrowFileList()
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

        public bool ApplyBatchBorrow(string borrowId, ApplyBorrowTable obj, string departmentId, string operateName)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    var recordList = conn.GetByWhere<Record>(" where DepartmentCode=@DepartmentCode",
                        new {DepartmentCode = departmentId}).ToList();

                    foreach (var item in recordList)
                    {
                        var data = new ApplyBorrowFileList()
                        {
                            BorrowID = borrowId,
                            RecordID = item.RecordID
                        };

                        var operate = new OperateLog()
                        {
                            RecordId = item.RecordID,
                            OperatePeople = operateName,
                            OperateType = "申请借阅",
                            OperateTime = DateTime.Now,
                            OperateInfo = $"申请借阅 借阅号:{borrowId},档案编号:{item.RecordID}"
                        };
                        conn.Insert<OperateLog>(operate, transaction);
                        

                        conn.Insert(data, transaction);

                        //更改档案状态
                        //item.RecordStatus = 3;
                        //conn.UpdateById(item, "RecordStatus", transaction);
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

        public bool ApplyBorrowAgree(string borrowId)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var transaction = conn.BeginTransaction();
                try
                {
                    var recordIdList = conn.GetByWhere<ApplyBorrowFileList>(" where BorrowID=@BorrowID",
                        new {BorrowID = borrowId}, null, null, transaction).Select(item => item.RecordID);

                    foreach (var recordId in recordIdList)
                    {
                        var record = conn.GetById<Record>(recordId, null, transaction);
                        record.RecordStatus = 3;
                        conn.UpdateById(record, "RecordStatus", transaction);
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    Log.WriteLog(e);
                    return false;
                }
            }
        }
    }
}
