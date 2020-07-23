using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IRepository.RecordTrancation;
using RightControl.IService;
using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class RecordListService:BaseService<RecordList>, IRecordListService
    {
        private readonly IRecordListRepository _recordListRepository;
        private readonly IFreeSql fsql;
        public RecordListService(IRecordListRepository recordListRepository, IFreeSql freeSql)
        {
            _recordListRepository = recordListRepository;
            fsql = freeSql;
        }
        public dynamic GetListByFilter(RecordList recordList, PageInfo pageInfo)
        {
            var where = " where 1=1";

            if (!string.IsNullOrEmpty(recordList.RecordId))
            {
                where += " and RecordId=@RecordId";
            }

            return base.GetListByFilter(recordList, pageInfo, where);
        }

        public List<RecordList> GetRecordListExpired(string recordId)
        {
            var time = DateTime.Now;
            //var str = fsql.Select<RecordList>().LeftJoin(a => a.FileID == a.File.FileID).From<ExpiredFileVerifyEntity>((s, b) => s
            //    .LeftJoin(a => a.ID == b.RecordFileId))
            //    .Where((s, b) => s.ExpirationTime < time)
            //    .Where((s, b) => b.Status == null)
            //    .Where((s, b) => s.RecordId == recordId).ToSql();
            //var recordList = fsql.Select<RecordList>().LeftJoin(a => a.FileID == a.File.FileID).From<ExpiredFileVerifyEntity>((s, b) => s
            //    .LeftJoin(a => a.ID == b.RecordFileId))
            //    .Where((s, b) => s.ExpirationTime < time)
            //    .Where((s, b) => b.Status == null)
            //    .Where((s, b) => s.RecordId == recordId)
            //    .ToList((s, b) => s);

            var recordList = fsql.Select<RecordList>()
                .Where(a => !fsql.Select<ExpiredFileVerifyEntity>().As("b").Where(i => i.Status == 0).ToList(b => b.RecordFileId).Contains(a.ID))
                .Where(s => s.ExpirationTime < time)
                .Where(s => s.RecordId == recordId)
                .Include(a => a.File)
                .ToList();

            return recordList;
            //return _recordListRepository.GetRecordListExpired(recordId);
        }
    }
}
