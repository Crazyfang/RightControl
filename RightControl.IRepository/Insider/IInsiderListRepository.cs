using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.Insider;

namespace RightControl.IRepository.Insider
{
    public interface IInsiderListRepository : IBaseRepository<YG_InsiderList>
    {
        IEnumerable<dynamic> GetInsiderList(string dep);
    }
}
