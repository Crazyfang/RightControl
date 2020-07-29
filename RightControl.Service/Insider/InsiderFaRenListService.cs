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
    public class InsiderFaRenListService : BaseService<YG_InsiderFaRenList>, IInsiderFaRenListService
    {
        public IInsiderFaRenListRepository Repository { get; set; }
        public IUserRepository userRepository { get; set; }

        public dynamic GetListByFilter(YG_InsiderFaRenList filter, PageInfo pageInfo)
        {
            var _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.FaRenNm))
            {
                _where += string.Format(" and FaRenNm like '%{0}%'", filter.FaRenNm);
            }

            return GetListByFilter(filter, pageInfo, _where);
        }
    }
}
