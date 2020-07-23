using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.RecordTrancation;

namespace RightControl.IService.RecordTrancation
{
    public interface IRecordListService:IBaseService<RecordList>
    {
        List<RecordList> GetRecordListExpired(string recordId);
        
    }
}
