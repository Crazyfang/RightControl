using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model;
using RightControl.Model.InforSearch;

namespace RightControl.IRepository.InforSearch
{
    public interface IInforSearchOperateRepository:IBaseRepository<InforSearchOperate>
    {
        IEnumerable<InforSearchOperate> GetPageMulTable(SearchFilter filter, out long total);
    }
}
