using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.RecordSetting;

namespace RightControl.IRepository.RecordSetting
{
    public interface ISelectTypeNameRepository:IBaseRepository<SelectTypeName>
    {
        bool DeleteSelectTypeName(int id);
    }
}
