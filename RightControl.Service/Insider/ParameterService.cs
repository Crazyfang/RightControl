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
    public class ParameterService : BaseService<XD_Parameter>, IParameterService
    {
        public IParameterRepository Repository { get; set; }
        public IUserRepository userRepository { get; set; }

        public dynamic GetListByFilter(XD_Parameter filter, PageInfo pageInfo)
        {
            var _where = " where 1=1";
           _where += string.Format(" and R_No = '{0}'", filter.R_No);

            return GetListByFilter(filter, pageInfo, _where);
        }
    }
}
