using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.RecordTrancation;

namespace RightControl.IService.RecordTrancation
{
    public interface ISelectTypeService:IBaseService<SelectType>
    {
        List<SelectType> GetSelectTypeName(int recordTypeId);
    }
}
