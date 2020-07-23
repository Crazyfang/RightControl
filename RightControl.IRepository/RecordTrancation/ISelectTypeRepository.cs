using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.RecordTrancation;

namespace RightControl.IRepository.RecordTrancation
{
    public interface ISelectTypeRepository:IBaseRepository<SelectType>
    {
        List<SelectType> GetSelectTypeName(int recordTypeId);
    }
}
