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
    public class SelectTypeRepository:BaseRepository<SelectType>, ISelectTypeRepository
    {
        public List<SelectType> GetSelectTypeName(int recordTypeId)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                string sql = "select a.*, b.RecordTypeName as RecordFileTypeName " +
                             "from SelectType a " +
                             "left join RecordFileTypeTable b " +
                             "on a.FileType = b.ID ";
                if (recordTypeId != 0)
                {
                    sql += $"where a.SelectTypeNum = {recordTypeId}";
                }
                return conn.Query<SelectType>(sql).ToList();
            }
        }
    }
}
