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
    public class RecordFileTypeService:BaseService<RecordFileType>, IRecordFileTypeService
    {
        public IRecordFileTypeRepository repository { get; set; }
        public dynamic GetListByFilter(RecordFileType filter, PageInfo pageInfo)
        {
            string _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.RecordFileTypeString))
            {
                _where += " and RecordFileTypeString=@RecordFileTypeString";
            }
            return GetListByFilter(filter, pageInfo, _where);
        }

        public int GetNumByStr(string str)
        {
            return repository.GetByWhere(" where RecordFileTypeString=@RecordFileTypeString", new {RecordFileTypeString = str})
                .FirstOrDefault().ID;
        }

        public int CreateModelReturnId(RecordFileType model)
        {
            return repository.Create(model);
        }

        public RecordFileType GetMaxTypeString()
        {
            return repository.GetMaxTypeString();
        }

        public bool DeleteFileType(int id)
        {
            return repository.DeleteFileType(id);
        }
    }
}
