using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model;
using RightControl.Model.InforSearch;

namespace RightControl.IService.InforSearch
{
    public interface IInforSearchOperateService:IBaseService<InforSearchOperate>
    {
        dynamic GetPageMulTable(InforSearchOperate filter, PageInfo pageInfo);
    }
}
