using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.RecordTrancation;

namespace RightControl.IRepository.RecordTrancation
{
    public interface IRecordFileTypeRepository:IBaseRepository<RecordFileType>
    {
        List<RecordFileType> FileTypeOfUnselected(int Id);

        RecordFileType GetMaxTypeString();

        bool DeleteFileType(int id);
    }
}
