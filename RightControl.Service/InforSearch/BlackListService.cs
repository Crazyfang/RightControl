using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IRepository;
using RightControl.IRepository.InforSearch;
using RightControl.IService.InforSearch;
using RightControl.Model;
using RightControl.Model.InforSearch;

namespace RightControl.Service.InforSearch
{
    public class BlackListService:BaseService<BlackList>, IBlackListService
    {
        public IBlackListRepository Repository { get; set; }
        public IUserRepository userRepository { get; set; }

        public dynamic GetListByFilter(BlackList filter, PageInfo pageInfo)
        {
            var _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.BankName))
            {
                //filter.BankName = "%" + filter.BankName + "%"; 
                _where += string.Format(" and BankName like '%{0}%'", filter.BankName);
            }

            return GetListByFilter(filter, pageInfo, _where);
        }
    }
}
