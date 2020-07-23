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
    public class InsiderWarnService : BaseService<YG_InsiderWarn>, IInsiderWarnService
    {
        public IInsiderWarnRepository Repository { get; set; }
        public IUserRepository userRepository { get; set; }

        public dynamic GetListByFilter(YG_InsiderWarn filter, PageInfo pageInfo)
        {
            pageInfo.field = "DataDate";
            pageInfo.order = "desc";
            var _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.R_Name))
            {
                _where += string.Format(" and R_Name like '%{0}%'", filter.R_Name);
            }
            if (!string.IsNullOrEmpty(filter.Insider))
            {
                _where += string.Format(" and Insider like '%{0}%'", filter.Insider);
            }
            if (!string.IsNullOrEmpty(filter.SearchTime) && filter.SearchTime != " ~ ")
            {
                if (filter.SearchTime.Contains("+"))
                {
                    filter.SearchTime = filter.SearchTime.Replace("+", "");
                }
                var dts = filter.SearchTime.Trim().Split('~');
                var start = dts[0].Trim();
                var end = dts[1].Trim();
                if (!string.IsNullOrEmpty(start))
                {
                    _where += string.Format(" and DataDate>='{0}'", start);
                }
                if (!string.IsNullOrEmpty(end))
                {
                    _where += string.Format(" and DataDate<='{0}'", end);
                }
            }

            return GetListByFilter(filter, pageInfo, _where);
        }
    }
}
