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
    public class FileListRepository:BaseRepository<FileList>, IFileListRepository
    {
        public List<FileList> GetFileListByFileTypeId(int fileTypeId)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var sql = $"select * from FileList where RecordFileType={fileTypeId}";
                return conn.Query<FileList>(sql).ToList();
            }
        }
    }
}
