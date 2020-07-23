using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IRepository;
using RightControl.IRepository.PfLender;
using RightControl.IService.PfLender;
using RightControl.Model;
using RightControl.Model.PfLender;

namespace RightControl.Service.PfLender
{
    public class LenderListService:BaseService<LenderList>,ILenderListService
    {
        public ILenderListRepository Repository { get; set; }
        public IUserRepository userRepository { get; set; }

        public dynamic GetListByFilter(LenderList filter, PageInfo pageInfo)
        {
            var _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.Name) || !string.IsNullOrEmpty(filter.IdentityCd))
            {
                _where += string.Format(" and Name like '%{0}%'", filter.Name);
                _where += string.Format(" and IdentityCd like '%{0}%'", filter.IdentityCd);
            }

            return GetListByFilter(filter, pageInfo, _where);
        }
    }
}
