using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions.SqlServerExt;
using RightControl.Common;
using RightControl.IRepository.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Repository.RecordTrancation
{
    public class RecordRepository:BaseRepository<Record>,IRecordRepository
    {
        public bool InsertRecord(Record record, List<RecordList> filelist, List<OtherFileList> otherFileList)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    //插入档案信息
                    conn.Insert<Record>(record, transaction);

                    //循环插入已维护文件清单信息
                    foreach (var file in filelist)
                    {
                        conn.Insert<RecordList>(file, transaction);
                    }

                    //循环插入其他文件清单信息
                    foreach (var otherfile in otherFileList)
                    {
                        conn.Insert<OtherFileList>(otherfile, transaction);
                    }
                    
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

        public List<RecordList> GetRecordListByRecordId(string recordId)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var sql = "select a.*, b.FileName as RecordFileName, c.HoldingCell, c.OriginalRecordType from RecordList a left join FileList b on a.FileID=b.FileID left join ContractFileType c on a.RecordType=c.ID where a.RecordID=@RecordID";
                return conn.Query<RecordList>(sql, new {RecordID = recordId}).ToList();
            }
        }

        public List<OtherFileList> GetOtherFileListByRecordId(string recordId)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var sql = "select a.*,b.HoldingCell, b.OriginalRecordType from OtherFileListTable a left join ContractFileType b on a.RecordFileType=b.ID where a.RecordID=@RecordID";
                return conn.Query<OtherFileList>(sql, new { RecordID = recordId }).ToList();
            }
        }

        public bool EditRecordNotChangeType(List<string> fileIdList, List<string> otherFileIdList, List<OtherFileList> otherFileList,
            List<RecordList> fileList, Record record, OperateLog operateLog)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    //更新档案信息
                    conn.UpdateById(record, null, transaction);

                    foreach (var temp in fileList)
                    {
                        //当前已维护文件清单被包含在档案已有同类型文件清单的编号中，则更新
                        if (fileIdList.Contains(temp.OriginalRecordType + "_" + temp.FileID + "_" + temp.RecordType))
                        {
                            var file = conn.GetByWhere<RecordList>(" where FileID='" + temp.FileID + "' and RecordID='" + temp.RecordId + "' and RecordType=@RecordType", new { RecordType = temp.RecordType }, null, null, transaction).First();
                            temp.ID = file.ID;
                            //conn.UpdateByWhere<RecordList>(
                            //    " where FileID='" + temp.FileID + "' and RecordID='" + temp.RecordId + "' and RecordType=@RecordType",
                            //    "Amount,ExpirationTime",
                            //    new {Amount = temp.Amount, ExpirationTime = temp.ExpirationTime, RecordType = temp.RecordType },
                            //    transaction);

                            if(file.Amount == temp.Amount && file.ExpirationTime == temp.ExpirationTime)
                            {

                            }
                            else
                            {
                                //var contractFileType = conn.GetById<ContractFileType>(temp.RecordType);
                                //var recordFileType = conn.GetById<RecordFileType>(contractFileType.RecordTypeID);
                                var fileName = conn.GetById<FileList>(temp.FileID, null, transaction);

                                operateLog.OperateInfo += $"修改预设文件:{fileName.FileName} 原数量:{file.Amount} 现数量:{temp.Amount} 原过期时间:{file.ExpirationTime} 现过期时间:{temp.ExpirationTime} <br>";
                                conn.UpdateById<RecordList>(temp, "Amount,ExpirationTime", transaction);
                            }
                            

                            //移除包含文件清单编号
                            fileIdList.Remove(temp.OriginalRecordType + "_" + temp.FileID + "_" + temp.RecordType);
                        }
                        //不包含，则插入
                        else
                        {
                            var fileName = conn.GetById<FileList>(temp.FileID, null, transaction);
                            operateLog.OperateInfo += $"新增预设文件:{fileName.FileName} 数量:{temp.Amount} 过期时间:{temp.ExpirationTime} <br>";
                            conn.Insert<RecordList>(temp, transaction);
                        }
                    }

                    //删除已被用户取消勾选的文件清单
                    foreach (var temp in fileIdList)
                    {
                        var file = conn.GetByWhere<RecordList>(" where RecordID=@RecordID and FileID=@FileID and RecordType=@RecordType", new { RecordID = record.RecordID, FileID = temp.Split('_')[1], RecordType = temp.Split('_')[2] }, null, null, transaction).First();
                        var fileName = conn.GetById<FileList>(file.FileID, null, transaction);
                        operateLog.OperateInfo += $"删除预设文件:{fileName.FileName} <br>";

                        conn.DeleteByWhere<RecordList>(" where RecordID=@RecordID and FileID=@FileID and RecordType=@RecordType", new {RecordID = record.RecordID, FileID = temp.Split('_')[1], RecordType=temp.Split('_')[2]}, transaction);
                        var exist = conn.Query<ContractFileType>("select a.* from ContractFileType a inner join RecordList b on a.ID=b.RecordType where a.ID=@ID union select a.* from ContractFileType a inner join OtherFileListTable b on a.ID=b.RecordFileType where a.ID=@ID", new { ID = temp.Split('_')[2] }, transaction).FirstOrDefault();
                        if(exist == null)
                        {
                            conn.DeleteById<ContractFileType>(int.Parse(temp.Split('_')[2]), transaction);
                        }
                    }

                    foreach (var temp in otherFileList)
                    {
                        //当前其他文件清单被包含在档案已有同类型文件清单的编号中，则更新
                        if (otherFileIdList.Contains(temp.OriginalRecordType + "_" + temp.ID))
                        {
                            var oldFile = conn.GetById<OtherFileList>(temp.ID, null, transaction);

                            if(oldFile.Amount == temp.Amount && temp.ExpirationTime == oldFile.ExpirationTime)
                            {

                            }
                            else
                            {
                                operateLog.OperateInfo += $"修改用户自定义文件:{oldFile.FileName} 原数量:{oldFile.Amount} 原过期时间:{oldFile.ExpirationTime} 现数量:{temp.Amount} 现过期时间:{temp.ExpirationTime} <br>";
                                conn.UpdateById(temp, null, transaction);
                            }
                            
                            otherFileIdList.Remove(temp.OriginalRecordType + "_" + temp.ID);
                        }
                        //不包含，则插入
                        else
                        {
                            operateLog.OperateInfo += $"新增用户自定义文件:{temp.FileName} 数量:{temp.Amount} 过期时间:{temp.ExpirationTime} <br>";
                            conn.Insert<OtherFileList>(temp, transaction);
                        }
                    }

                    //删除已被用户取消填写的其他文件清单
                    foreach (var temp in otherFileIdList)
                    {
                        var otherFile = conn.GetById<OtherFileList>(int.Parse(temp.Split('_')[1]), null, transaction);
                        operateLog.OperateInfo += $"删除用户自定义文件:{otherFile.FileName} 数量:{otherFile.Amount} 过期时间:{otherFile.ExpirationTime} <br>";
                        conn.DeleteById<OtherFileList>(int.Parse(temp.Split('_')[1]), transaction);

                        var exist = conn.Query<ContractFileType>("select a.* from ContractFileType a inner join RecordList b on a.ID=b.RecordType where a.RecordID=@RecordID and OriginalRecordType=@OriginalRecordType union select a.* from ContractFileType a inner join OtherFileListTable b on a.ID=b.RecordFileType where a.RecordID=@RecordID and OriginalRecordType=@OriginalRecordType", new { RecordID =record.RecordID, OriginalRecordType = temp.Split('_')[1] }, transaction).FirstOrDefault();
                        if (exist == null)
                        {
                            conn.DeleteById<ContractFileType>(int.Parse(temp.Split('_')[1]), transaction);
                        }
                    }

                    conn.Insert(operateLog, transaction);
                    //事务提交
                    transaction.Commit();
                    return true;
                }
                catch (System.Exception e)
                {
                    Log.WriteLog(e);
                    //事务回滚
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool EditRecordChangeType(List<OtherFileList> otherFileList, List<RecordList> fileList, Record record, OperateLog operaterLog)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    //更新档案记录
                    conn.UpdateById(record, null, transaction);
                    //删除已维护档案清单的所有记录
                    conn.DeleteByWhere<RecordList>(" where RecordID=@RecordID", new {RecordID = record.RecordID},
                        transaction);
                    //删除其他档案清单的所有记录
                    conn.DeleteByWhere<OtherFileList>(" where RecordID=@RecordID", new {RecordID = record.RecordID},
                        transaction);
                    //删除档案类别表
                    conn.DeleteByWhere<ContractFileType>(" where RecordID=@RecordID", new { RecordID = record.RecordID },
                        transaction);

                    //添加其他档案清单
                    foreach (var temp in otherFileList)
                    {
                        operaterLog.OperateInfo += $"新增自定义文件 文件名称:{temp.FileName} 数量:{temp.Amount} 过期时间:{temp.ExpirationTime} <br>";
                        conn.Insert<OtherFileList>(temp, transaction);
                    }
                    //添加已维护档案清单
                    foreach (var temp in fileList)
                    {
                        var fileName = conn.GetById<FileList>(temp.FileID, null, transaction);
                        operaterLog.OperateInfo += $"新增预设文件 文件名称:{fileName.FileName} 数量:{temp.Amount} 过期时间:{temp.ExpirationTime} <br>";
                        conn.Insert<RecordList>(temp, transaction);
                    }

                    conn.Insert<OperateLog>(operaterLog, transaction);
                    //事务提交
                    transaction.Commit();
                    return true;
                }
                catch (System.Exception e)
                {
                    Log.WriteLog(e);
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public List<ContractFileType> GetTheTypeList(string recordId)
        {
            using (var conn=SqlHelper.SqlConnection())
            {
                //var sql =
                //    "select c.* from RecordList a left join FileList b on a.FileID = b.FileID left join RecordFileTypeTable c on b.RecordFileType = c.ID where RecordID = @RecordID union select b.* from OtherFileListTable a left join RecordFileTypeTable b on a.RecordFileType = b.ID where RecordID=@RecordID order by RecordFileTypeString";
                var sql = "select a.*,b.RecordTypeName from ContractFileType a left join RecordFileTypeTable b on a.RecordTypeID=b.ID where RecordID = @RecordID";
                return conn.Query<ContractFileType>(sql, new {RecordID = recordId}).ToList();
            }
        }

        public Record GetRecord(string recordId)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var sql = "select a.*, b.RealName as RecordManagerName, c.DepartmentName as RecordDepartmentName from RecordTable a left join t_user b on a.RecordManager=b.UserName left join t_department c on a.RecordManagerDepartment=c.DepartmentCode where a.RecordID=@RecordID";
                return conn.QueryFirst<Record>(sql, new {RecordID = recordId});
            }
        }

        public bool DeleteRecord(string recordId)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    var record = conn.GetById<Record>(recordId, null, transaction);

                    var operateLog = new OperateLog()
                    {
                        OperateType = "删除",
                        RecordId = recordId
                    };

                    operateLog.OperateInfo += $"删除档案 档案编号:{record.RecordID} 档案用户姓名:{record.RecordUserName} 档案用户客户号:{record.RecordUserCode} ";
                    //删除档案记录
                    conn.DeleteById<Record>(recordId, transaction);
                    //删除已维护档案文件
                    conn.DeleteByWhere<RecordList>(" where RecordID=@RecordID", new {RecordID = recordId}, transaction);
                    //删除为维护档案文件
                    conn.DeleteByWhere<OtherFileList>(" where RecordID=@RecordID", new {RecordID = recordId}, transaction);
                    conn.DeleteByWhere<ContractFileType>(" where RecordID=@RecordID", new { RecordID = recordId }, transaction);
                    conn.Insert(operateLog, transaction);
                    transaction.Commit();
                    return true;
                }
                catch (System.Exception e)
                {
                    Log.WriteLog(e);
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public dynamic GetRecordInfoDif()
        {
            string sql = "select sum(case when RecordStatus = 1 then 1 else 0 end) as RecordHand," +
                         "sum(case when RecordStatus = 2 then 1 else 0 end) as RecordIn," +
                         "sum(case when RecordStatus = 3 then 1 else 0 end) as RecordOut" +
                         " from RecordTable";
            using (var conn = SqlHelper.SqlConnection())
            {
                return conn.QueryFirst(sql);
            }
        }

        public List<Record> GetExpiredRecord()
        {
            var sql =
                "select a.* from RecordTable a left join RecordList b on a.RecordID=b.RecordID " +
                "left join OtherFileListTable c " +
                "on a.RecordID=c.RecordID " +
                "where b.ExpirationTime < GETDATE() or c.ExpirationTime < GETDATE()";
            using (var conn = SqlHelper.SqlConnection())
            {
                return conn.Query<Record>(sql).ToList();
            }
        }

        public IEnumerable<Record> GetPageMulTable(SearchFilter filter, out long total)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                return conn.GetByPageRecordExpired<Record>(typeof(Record), filter.pageIndex, filter.pageSize, out total, filter.returnFields, filter.where, filter.param, filter.orderBy, filter.transaction, filter.commandTimeout);
            }
        }

        public bool ChangeRecordBelong(string originalUser, string nowUser)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    var sql = "update RecordTable set RecordManager=@originalUser where RecordManager=@nowUser";
                    conn.Execute(sql, new {originalUser = originalUser, nowUser = nowUser}, transaction);

                    transaction.Commit();
                    return true;
                }
                catch (System.Exception e)
                {
                    Log.WriteLog(e);
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
