using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IRepository;
using RightControl.IRepository.Insider;
using RightControl.IService.Insider;
using RightControl.Model;
using RightControl.Model.Insider;

namespace RightControl.Service.Insider
{
    public class ModuleService : BaseService<Module>, IModuleService
    {
        public IModuleRepository repository { get; set; }
        public IEnumerable<Module> GetModule()
        {
            return repository.GetModule();
        }

        public dynamic GetListByFilter(Module filter, PageInfo pageInfo)
        {
            string _where = " where 1=1";
            if (filter.No!=0)
            {
                _where += " and No=@No";
            }
            return GetListByFilter(filter, pageInfo, _where);
        }
    }
}
