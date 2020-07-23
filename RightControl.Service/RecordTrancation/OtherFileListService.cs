using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IRepository.RecordTrancation;
using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class OtherFileListService:BaseService<OtherFileList>, IOtherFileListService
    {
        private readonly IOtherFileListRepository _otherFileListRepository;
        private readonly IFreeSql fsql;
        public OtherFileListService(IOtherFileListRepository otherFileListRepository, IFreeSql freeSql)
        {
            _otherFileListRepository = otherFileListRepository;
            fsql = freeSql;
        }
        
        public dynamic GetListByFilter(OtherFileList filter, PageInfo pageInfo)
        {
            string _where = " where 1=1";
            if (string.IsNullOrEmpty(filter.FileName))
            {
                _where += " and FileName=@FileName";
            }

            return GetListByFilter(filter, pageInfo, _where);
        }

        public List<OtherFileList> GetExpiredFile(string recordId)
        {
            var time = DateTime.Now;
            //var recordList = fsql.Select<OtherFileList>().From<ExpiredFileVerifyEntity>((s, b) => s
            //    .LeftJoin(a => a.ID == b.OtherFileId))
            //    .Where((s, b) => s.ExpirationTime < time)
            //    .Where((s, b) => b.Status == null)
            //    .Where((s, b) => s.RecordID == recordId)
            //    .ToList((s, b) => s);

            var recordList = fsql.Select<OtherFileList>()
                .Where(a => !fsql.Select<ExpiredFileVerifyEntity>().As("b").Where(i => i.Status == 0).ToList(b => b.OtherFileId).Contains(a.ID))
                .Where(s => s.ExpirationTime < time)
                .Where(s => s.RecordID == recordId)
                .ToList();

            return recordList;
        }
    }
}
