using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using RightControl.IRepository.RecordTrancation;
using RightControl.Model.RecordTrancation;

namespace RightControl.Repository.RecordTrancation
{
    public class RecordListRepository:BaseRepository<RecordList>, IRecordListRepository
    {
        public List<RecordList> GetRecordListExpired(string recordId)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var sql =
                    "select a.*, b.FileName as RecordFileName from RecordList a left join FileList b on a.FileID=b.FileID where a.RecordID=@RecordID and ExpirationTime < GETDATE()";

                return conn.Query<RecordList>(sql, new {RecordID = recordId}).ToList();
            }
        }
    }
}
