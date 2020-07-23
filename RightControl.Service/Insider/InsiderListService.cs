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
    public class InsiderListService : BaseService<YG_InsiderList>, IInsiderListService
    {
        public IInsiderListRepository Repository { get; set; }
        public IUserRepository userRepository { get; set; }

        public dynamic GetListByFilter(YG_InsiderList filter, PageInfo pageInfo)
        {
            var _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.RepartyNm))
            {
                _where += string.Format(" and RepartyNm like '%{0}%'", filter.RepartyNm);
            }
            if (!string.IsNullOrEmpty(filter.Department))
            {
                _where += string.Format(" and Department like '%{0}%'", filter.Department);
            }

            return GetListByFilter(filter, pageInfo, _where);
        }

        public IEnumerable<dynamic> GetInsiderList(string dep)
        {
            return Repository.GetInsiderList(dep);
        }
    }
}
