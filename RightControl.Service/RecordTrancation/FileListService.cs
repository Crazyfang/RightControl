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
    public class FileListService:BaseService<FileList>, IFileListService
    {
        public IFileListRepository repository { get; set; }
        public dynamic GetListByFilter(FileList filter, PageInfo pageInfo)
        {
            string _where = " where 1=1";
            if (filter.ActiveMark)
            {
                _where += " and ActiveMark=@ActiveMark";
            }

            if (filter.RecordFileType != 0)
            {
                _where += " and RecordFileType=@RecordFileType";
            }
            return GetListByFilter(filter, pageInfo, _where);
        }

        public List<FileList> GetFileListByFileTypeId(int fileTypeId)
        {
            return repository.GetFileListByFileTypeId(fileTypeId);
        }
    }
}
