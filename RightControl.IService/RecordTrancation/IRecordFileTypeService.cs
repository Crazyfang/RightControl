using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.RecordTrancation;

namespace RightControl.IService.RecordTrancation
{
    public interface IRecordFileTypeService:IBaseService<RecordFileType>
    {
        int GetNumByStr(string str);

        int CreateModelReturnId(RecordFileType model);

        RecordFileType GetMaxTypeString();

        bool DeleteFileType(int id);
    }
}
