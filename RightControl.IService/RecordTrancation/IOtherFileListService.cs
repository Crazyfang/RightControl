using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.RecordTrancation;

namespace RightControl.IService.RecordTrancation
{
    public interface IOtherFileListService:IBaseService<OtherFileList>
    {
        List<OtherFileList> GetExpiredFile(string recordId);
    }
}
