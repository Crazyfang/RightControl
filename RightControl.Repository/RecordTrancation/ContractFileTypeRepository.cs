using RightControl.IRepository.RecordTrancation;
using RightControl.Model.RecordTrancation;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace RightControl.Repository.RecordTrancation
{
    public class ContractFileTypeRepository:BaseRepository<ContractFileType>, IContractFileTypeRepository
    {
        public List<ContractFileType> GetListByRecordId(string recordId)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var sql =
                    "select a.*, b.RecordTypeName from ContractFileType a left join RecordFileTypeTable b on a.RecordTypeID=b.ID where a.RecordID=@RecordID";
                return conn.Query<ContractFileType>(sql, new {RecordID = recordId}).ToList();
            }
        }
    }
}
