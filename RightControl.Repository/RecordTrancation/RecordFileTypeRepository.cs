using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using RightControl.IRepository.RecordTrancation;
using RightControl.Model.RecordTrancation;

namespace RightControl.Repository.RecordTrancation
{
    public class RecordFileTypeRepository:BaseRepository<RecordFileType>, IRecordFileTypeRepository
    {
        public List<RecordFileType> FileTypeOfUnselected(int Id)
        {
            using (var conn=SqlHelper.SqlConnection())
            {
                var sql =
                    $"  select * from RecordFileTypeTable where ID not in (select FileType from SelectType where SelectTypeNum = {Id})";

                return conn.Query<RecordFileType>(sql).ToList();
            }
        }

        public RecordFileType GetMaxTypeString()
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var sql = $"select * from RecordFileTypeTable order by RecordFileTypeString desc";

                return conn.Query<RecordFileType>(sql).First();
            }
        }

        public bool DeleteFileType(int id)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    var sql1 = $"delete from FileList where RecordFileType={id}";
                    var sql2 = $"delete from RecordFileTypeTable where ID={id}";

                    conn.Execute(sql1, null, transaction);
                    conn.Execute(sql2, null, transaction);

                    transaction.Commit();
                    return true;
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
