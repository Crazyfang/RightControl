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
    public class InsiderRltsListService : BaseService<YG_InsiderRltsList>, IInsiderRltsListService
    {
        public IInsideRltsListRepository Repository { get; set; }
        public IUserRepository userRepository { get; set; }

        public dynamic GetListByFilter(YG_InsiderRltsList filter, PageInfo pageInfo)
        {
            var _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.Department))
            {
                _where += string.Format(" and Department like '%{0}%'", filter.Department);
            }
            if (!string.IsNullOrEmpty(filter.InsiderNm))
            {
                _where += string.Format(" and InsiderNm like '%{0}%'", filter.InsiderNm);
            }

            return GetListByFilter(filter, pageInfo, _where);
        }
    }
}
