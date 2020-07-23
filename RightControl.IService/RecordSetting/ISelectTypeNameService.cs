using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.RecordSetting;

namespace RightControl.IService.RecordSetting
{
    public interface ISelectTypeNameService:IBaseService<SelectTypeName>
    {
        int CreateModelReturnId(SelectTypeName model);

        bool DeleteSelectTypeName(int id);
    }
}
