using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RightControl.IRepository.RecordTrancation;
using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.Permissions;
using RightControl.Model.RecordTrancation;
using RightControl.Model.RecordTrancation.RecordDto.Output;

namespace RightControl.Service.RecordTrancation
{
    public class RecordService:BaseService<Record>, IRecordService
    {
        private IRecordRepository repository;
        private IGetNumRepository getnumrepository;
        private IFreeSql fsql;

        public RecordService(IRecordRepository recordRepository, IGetNumRepository getNumRepository, IFreeSql freeSql)
        {
            repository = recordRepository;
            getnumrepository = getNumRepository;
            fsql = freeSql;
        }

        public dynamic GetListByFilter(Record filter, PageInfo pageInfo)
        {
            string _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.RecordManagerDepartment))
            {
                _where += " and RecordManagerDepartment=@RecordManagerDepartment";
            }
            if (!string.IsNullOrEmpty(filter.RecordManager))
            {
                _where += " and RecordManager=@RecordManager";
            }
            if (!string.IsNullOrEmpty(filter.RecordUserCode))
            {
                _where += " and RecordUserCode=@RecordUserCode";
            }
            if (!string.IsNullOrEmpty(filter.RecordUserName))
            {
                _where += " and RecordUserName=@RecordUserName";
            }
            if (filter.RecordStatus != 0)
            {
                _where += " and RecordStatus=@RecordStatus";
            }

            return GetListByFilter(filter, pageInfo, _where);
        }

        public int GetNum()
        {
            return getnumrepository.Create(new GetNum()
            {
                Msg = 1
            });
        }

        public bool InsertRecord(Record record, List<RecordList> filelist, List<OtherFileList> otherFileList)
        {
            return repository.InsertRecord(record, filelist, otherFileList);
        }

        public List<RecordList> GetRecordListByRecordId(string recordId)
        {
            return repository.GetRecordListByRecordId(recordId);
        }

        public List<OtherFileList> GetOtherFileListByRecordId(string recordId)
        {
            return repository.GetOtherFileListByRecordId(recordId);
        }

        public bool EditRecordNotChangeType(List<string> fileIdList, List<string> otherFileIdList, List<OtherFileList> otherFileList,
            List<RecordList> fileList, Record record, OperateLog operateLog)
        {
            return repository.EditRecordNotChangeType(fileIdList, otherFileIdList, otherFileList, fileList, record, operateLog);
        }

        public bool EditRecordChangeType(List<OtherFileList> otherFileList, List<RecordList> fileList, Record record, OperateLog operateLog)
        {
            return repository.EditRecordChangeType(otherFileList, fileList, record, operateLog);
        }

        public List<ContractFileType> GetTheTypeList(string recordId)
        {
            return repository.GetTheTypeList(recordId);
        }

        public Record GetRecord(string recordId)
        {
            return repository.GetRecord(recordId);
        }

        public bool DeleteRecord(string recordId)
        {
            return repository.DeleteRecord(recordId);
        }

        public dynamic GetRecordInfoDif()
        {
            return repository.GetRecordInfoDif();
        }

        public List<Record> GetExpiredRecord()
        {
            return repository.GetExpiredRecord();
        }

        public dynamic GetPageMulTable(Record filter, PageInfo pageInfo)
        {
            string _orderBy = " ORDER BY a.RecordID desc";
            string where = string.Empty;
            

            where += "where b.ExpirationTime < GETDATE() or c.ExpirationTime < GETDATE()";
            long total = 0;
            var list = repository.GetPageMulTable(new SearchFilter { pageIndex = pageInfo.page, pageSize = pageInfo.limit, returnFields = pageInfo.returnFields, param = filter, where = where, orderBy = _orderBy }, out total);

            return new { code = 0, count = total, data = list };

            //return InforSearchOperateRepository.GetPageMulTable(filter, out total);
        }

        public bool ChangeRecordBelong(string originalUser, string nowUser)
        {
            return repository.ChangeRecordBelong(originalUser, nowUser);
        }

        public dynamic TestGetRecordPage(Record record, PageInfo pageInfo)
        {
            var data = fsql.Select<Record>().From<DepartmentModel, UserModel>((s, b, c) => s
                    .LeftJoin(a => a.RecordManagerDepartment == b.DepartmentCode)
                    .LeftJoin(a => a.RecordManager == c.UserName))
                    .WhereIf(!string.IsNullOrEmpty(record.RecordManagerDepartment), (a, b, c) => a.RecordManagerDepartment == record.RecordManagerDepartment)
                    .WhereIf(!string.IsNullOrEmpty(record.RecordManager), (a, b, c) => a.RecordManager == record.RecordManager)
                    .WhereIf(!string.IsNullOrEmpty(record.RecordUserCode), (a, b, c) => a.RecordUserCode == record.RecordUserCode)
                    .WhereIf(!string.IsNullOrEmpty(record.RecordUserName), (a, b, c) => a.RecordUserName == record.RecordUserName)
                    .WhereIf(record.RecordStatus != 0, (a, b, c) => a.RecordStatus == record.RecordStatus)
                    .Count(out var total)
                    .OrderBy((a, b, c) => a.RecordID)
                    .Page(pageInfo.page, pageInfo.limit)
                    .ToList((a, b, c) => new RecordOutput());

            //var data = fsql.Select<Record>()
            //    .WhereIf(record.RecordStatus != 0, i => i.RecordStatus == record.RecordStatus)
            //    .WhereIf(!string.IsNullOrEmpty(record.RecordManagerDepartment), i => i.RecordManagerDepartment == record.RecordManagerDepartment)
            //    .WhereIf(!string.IsNullOrEmpty(record.RecordManager), i => i.RecordManager == record.RecordManager)
            //    .WhereIf(!string.IsNullOrEmpty(record.RecordUserCode), i => i.RecordUserCode == record.RecordUserCode)
            //    .WhereIf(!string.IsNullOrEmpty(record.RecordUserName), i => i.RecordUserName == record.RecordUserName)
            //    .WhereIf(record.RecordStatus != 0, i => i.RecordStatus == record.RecordStatus)
            //    .Count(out var total)
            //    .OrderBy(i => i.RecordID)
            //    .Page(pageInfo.page, pageInfo.limit)
            //    .ToList();
            

            return Pager.Paging(data, total);
        }

        public dynamic RecordBelongList(string userCode)
        {
            var list = fsql.Select<Record>().From<UserModel>((s, b) => s
                .LeftJoin(a => a.RecordManager == b.UserName))
                .Where((s, b) => s.RecordManager == userCode )
                .ToList((s, b) => new { value = s.RecordID, label = s.RecordUserName });

            return list;
        }

        public bool RecordBelongChange(JArray changeRecordList, string operater)
        {
            try
            {
                fsql.Transaction(() =>
                {
                    foreach (var item in changeRecordList)
                    {
                        var userCode = item["value"].ToString();
                        var recordList = item["selectRecord"].Children().Select(a => a.Value<string>()).ToList();
                        var user = fsql.Select<UserModel>().Where(i => i.UserName == userCode).First();

                        foreach (var index in recordList)
                        {
                            var record = fsql.Select<Record>().Where(i => i.RecordID == index).First();
                            var oldUser = fsql.Select<UserModel>().Where(i => i.UserName == record.RecordManager).First();
                            var operateLog = new OperateLog()
                            {
                                RecordId = index,
                                OperatePeople = operater,
                                OperateTime = DateTime.Now,
                                OperateType = "归属关系变更",
                                OperateInfo = $"原归属人:{oldUser.RealName} 现归属人:{user.RealName}"
                            };

                            fsql.Insert(operateLog).ExecuteAffrows();
                        }

                        var affrows = fsql.Update<Record>()
                            .Set(a => a.RecordManager, userCode)
                            .Where(a => recordList.Contains(a.RecordID))
                            .ExecuteAffrows();

                        if (affrows < 1)
                        {
                            throw new Exception("更新失败");
                        }
                    }
                });

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public dynamic GetExpiredFileRecord(Record filter, PageInfo pageInfo)
        {
            var time = DateTime.Now;

            //var total = fsql.Select<Record>().From<RecordList, OtherFileList, ExpiredFileVerifyEntity>((s, b, c, d) => s
            //    .LeftJoin(a => a.RecordID == b.RecordId)
            //    .LeftJoin(a => a.RecordID == c.RecordID)
            //    .LeftJoin(a => b.ID == d.RecordFileId || c.ID == d.OtherFileId))
            //    .Where((s, b, c, d) => b.ExpirationTime < time || c.ExpirationTime < time)
            //    .Where((s, b, c, d) => d.Status == null || d.Status == 1 || d.Status == 2)
            //    .Distinct()
            //    .OrderBy((s, b, c, d) => s.RecordID)
            //    .ToList((s, b, c, d) => s).Count;

            //var recordList = fsql.Select<Record>().From<RecordList, OtherFileList, ExpiredFileVerifyEntity>((s, b, c, d) => s
            //    .LeftJoin(a => a.RecordID == b.RecordId)
            //    .LeftJoin(a => a.RecordID == c.RecordID)
            //    .LeftJoin(a => b.ID == d.RecordFileId || c.ID == d.OtherFileId))
            //    .Where((s, b, c, d) => b.ExpirationTime < time || c.ExpirationTime < time)
            //    .Where((s, b, c, d) => d.Status == null || d.Status == 1 || d.Status == 2)
            //    .Distinct()
            //    .OrderBy((s, b, c, d) => s.RecordID)
            //    .Page(pageInfo.page, pageInfo.limit)
            //    .ToList((s, b, c, d) => s);

            var total = fsql.Select<Record>().From<ExpiredFileVerifyEntity>((s, b) => s
                    .LeftJoin(a => a.RecordID == b.RecordItemId))
                    .Where((s, b) => fsql.Select<RecordList>().Where(i => i.ExpirationTime < time && i.RecordId == s.RecordID).Any())
                    .Where((s, b) => fsql.Select<OtherFileList>().Where(i => i.ExpirationTime < time && i.RecordID == s.RecordID).Any())
                    .Where((s, b) => b.Status == 2 || b.Status == 1 || b.Status == null)
                    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserName), (s, b) => s.RecordUserName == filter.RecordUserName)
                    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserCode), (s, b) => s.RecordUserCode == filter.RecordUserCode)
                    .Distinct()
                    .OrderBy((s, b) => s.RecordID)
                    .ToList((s, b) => s).Count();

            var recordList = fsql.Select<Record>().From<ExpiredFileVerifyEntity>((s, b) => s
                    .LeftJoin(a => a.RecordID == b.RecordItemId))
                    .Where((s, b) => fsql.Select<RecordList>().Where(i => i.ExpirationTime < time && i.RecordId == s.RecordID).Any())
                    .Where((s, b) => fsql.Select<OtherFileList>().Where(i => i.ExpirationTime < time && i.RecordID == s.RecordID).Any())
                    .Where((s, b) => b.Status == 2 || b.Status == 1 || b.Status == null)
                    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserName), (s, b) => s.RecordUserName == filter.RecordUserName)
                    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserCode), (s, b) => s.RecordUserCode == filter.RecordUserCode)
                    .Distinct()
                    .OrderBy((s, b) => s.RecordID)
                    .ToList((s, b) => s);

            return new { code = 0, count = total, data = recordList };
        }

        public dynamic GetExpiredFileRecord(Record filter, PageInfo pageInfo, string userCode)
        {
            var time = DateTime.Now;

            var sql = fsql.Select<Record>().From<ExpiredFileVerifyEntity>((s, b) => s
                    .LeftJoin(a => a.RecordID == b.RecordItemId))
                    .Where((s, b) => fsql.Select<RecordList>().Where(i => i.ExpirationTime < time && i.RecordId == s.RecordID).Any())
                    .Where((s, b) => fsql.Select<OtherFileList>().Where(i => i.ExpirationTime < time && i.RecordID == s.RecordID).Any())
                    .Where((s, b) => b.Status == 2 || b.Status == 1 || b.Status == null)
                    .Where((s, b) => s.RecordManager == userCode)
                    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserName), (s, b) => s.RecordUserName == filter.RecordUserName)
                    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserCode), (s, b) => s.RecordUserCode == filter.RecordUserCode)
                    .Distinct()
                    .OrderBy((s, b) => s.RecordID)
                    .ToSql();

            var total = fsql.Select<Record>().From<ExpiredFileVerifyEntity>((s, b) => s
                    .LeftJoin(a => a.RecordID == b.RecordItemId))
                    .Where((s, b) => fsql.Select<RecordList>().Where(i => i.ExpirationTime < time && i.RecordId == s.RecordID).Any())
                    .Where((s, b) => fsql.Select<OtherFileList>().Where(i => i.ExpirationTime < time && i.RecordID == s.RecordID).Any())
                    .Where((s, b) => b.Status == 2 || b.Status == 1 || b.Status == null)
                    .Where((s, b) => s.RecordManager == userCode)
                    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserName), (s, b) => s.RecordUserName == filter.RecordUserName)
                    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserCode), (s, b) => s.RecordUserCode == filter.RecordUserCode)
                    .Distinct()
                    .OrderBy((s, b) => s.RecordID)
                    .ToList((s, b) => s).Count();

            var recordList = fsql.Select<Record>().From<ExpiredFileVerifyEntity>((s, b) => s
                    .LeftJoin(a => a.RecordID == b.RecordItemId))
                    .Where((s, b) => fsql.Select<RecordList>().Where(i => i.ExpirationTime < time && i.RecordId == s.RecordID).Any())
                    .Where((s, b) => fsql.Select<OtherFileList>().Where(i => i.ExpirationTime < time && i.RecordID == s.RecordID).Any())
                    .Where((s, b) => b.Status == 2 || b.Status == 1 || b.Status == null)
                    .Where((s, b) => s.RecordManager == userCode)
                    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserName), (s, b) => s.RecordUserName == filter.RecordUserName)
                    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserCode), (s, b) => s.RecordUserCode == filter.RecordUserCode)
                    .Distinct()
                    .OrderBy((s, b) => s.RecordID)
                    .ToList((s, b) => s);

            //var sql = fsql.Select<Record>().From<RecordList, OtherFileList, ExpiredFileVerifyEntity>((s, b, c, d) => s
            //    .LeftJoin(a => a.RecordID == b.RecordId)
            //    .LeftJoin(a => a.RecordID == c.RecordID)
            //    .LeftJoin(a => b.ID == d.RecordFileId || c.ID == d.OtherFileId))
            //    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserName), (s, b, c, d) => s.RecordUserName == filter.RecordUserName)
            //    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserCode), (s, b, c, d) => s.RecordUserCode == filter.RecordUserCode)
            //    .Where((s, b, c, d) => s.RecordManager == userCode)
            //    .Where((s, b, c, d) => b.ExpirationTime < time || c.ExpirationTime < time)
            //    .Where((s, b, c, d) => d.Status == null || d.Status == 1 || d.Status == 2)
            //    .Distinct()
            //    .OrderBy((s, b, c, d) => s.RecordID)
            //    .ToSql();

            //var total = fsql.Select<Record>().From<RecordList, OtherFileList, ExpiredFileVerifyEntity>((s, b, c, d) => s
            //    .LeftJoin(a => a.RecordID == b.RecordId)
            //    .LeftJoin(a => a.RecordID == c.RecordID)
            //    .LeftJoin(a => b.ID == d.RecordFileId || c.ID == d.OtherFileId))
            //    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserName), (s, b, c, d) => s.RecordUserName == filter.RecordUserName)
            //    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserCode), (s, b, c, d) => s.RecordUserCode == filter.RecordUserCode)
            //    .Where((s, b, c, d) => s.RecordManager == userCode)
            //    .Where((s, b, c, d) => b.ExpirationTime < time || c.ExpirationTime < time)
            //    .Where((s, b, c, d) => d.Status == null || d.Status == 1 || d.Status == 2)
            //    .Distinct()
            //    .OrderBy((s, b, c, d) => s.RecordID)
            //    .ToList((s, b, c, d) => s).Count;

            //var recordList = fsql.Select<Record>().From<RecordList, OtherFileList, ExpiredFileVerifyEntity>((s, b, c, d) => s
            //    .LeftJoin(a => a.RecordID == b.RecordId)
            //    .LeftJoin(a => a.RecordID == c.RecordID)
            //    .LeftJoin(a => b.ID == d.RecordFileId || c.ID == d.OtherFileId))
            //    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserName), (s, b, c, d) => s.RecordUserName == filter.RecordUserName)
            //    .WhereIf(!string.IsNullOrEmpty(filter.RecordUserCode), (s, b, c, d) => s.RecordUserCode == filter.RecordUserCode)
            //    .Where((s, b, c, d) => b.ExpirationTime < time || c.ExpirationTime < time)
            //    .Where((s, b, c, d) => d.Status == null || d.Status == 1 || d.Status == 2)
            //    .Where((s, b, c, d) => s.RecordManager == userCode)
            //    .Distinct()
            //    .OrderBy((s, b, c, d) => s.RecordID)
            //    .Page(pageInfo.page, pageInfo.limit)
            //    .ToList((s, b, c, d) => s);

            return new { code = 0, count = total, data = recordList };
        }

        public dynamic NeedVerifyExpiredFileList(PageInfo pageInfo)
        {
            var total = fsql.Select<Record>().From<ExpiredFileVerifyEntity>((s, b) => s
                    .LeftJoin(a => a.RecordID == b.RecordItemId))
                    .Where((s, b) => b.Status == 0)
                    .Distinct()
                    .OrderBy((s, b) => s.RecordID)
                    .ToList((s, b) => s).Count();

            var result = fsql.Select<Record>().From<ExpiredFileVerifyEntity>((s, b) => s
                    .LeftJoin(a => a.RecordID == b.RecordItemId))
                    .Where((s, b) => b.Status == 0)
                    .Distinct()
                    .OrderBy((s, b) => s.RecordID)
                    .ToList((s, b) => new {
                        RecordId = s.RecordID,
                        RecordManager = s.RecordManager,
                        RecordManagerDepartment = s.RecordManagerDepartment,
                        ChangedDateTime = b.OperateTime,
                        Type = b.Type
                    });

            return new { code = 0, count = total, data = result };
        }

        public ExpiredClass GetExpiredFileCompare(string recordId)
        {
            var recordList = fsql.Select<RecordList>()
                .Where(a => fsql.Select<ExpiredFileVerifyEntity>().As("b").Where(i => i.Status == 0).ToList(b => b.RecordFileId).Contains(a.ID))
                .Where(s => s.RecordId == recordId)
                .Include(a => a.File)
                .ToList();

            var otherList = fsql.Select<OtherFileList>()
                .Where(a => fsql.Select<ExpiredFileVerifyEntity>().As("b").Where(i => i.Status == 0).ToList(b => b.OtherFileId).Contains(a.ID))
                .Where(s => s.RecordID == recordId)
                .ToList();

            var expiredVerifyList = fsql.Select<ExpiredFileVerifyEntity>()
                .Where(a => a.RecordFile.RecordId == recordId || a.OtherFile.RecordID == recordId)
                .ToList();

            return new ExpiredClass() { recordList = recordList, otherList = otherList, expiredVerifyList  = expiredVerifyList};
        }

        public List<OperateLog> GetRecordHistory(string recordId)
        {
            var list = fsql.Select<OperateLog>().Where(i => i.RecordId == recordId).OrderByDescending(i => i.OperateTime).ToList();

            return list;
        }
    }
}
