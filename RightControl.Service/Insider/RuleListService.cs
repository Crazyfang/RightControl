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
    public class RuleListService : BaseService<RuleList>, IRuleListService
    {
        public IRuleListRepository Repository { get; set; }
        public IUserRepository userRepository { get; set; }

        public dynamic GetListByFilter(RuleList filter, PageInfo pageInfo)
        {
            var _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.R_Name))
            {
                _where += string.Format(" and R_Name like '%{0}%'", filter.R_Name);
            }

            return GetListByFilter(filter, pageInfo, _where);
        }

        public IEnumerable<RuleList> GetRule()
        {
            return Repository.GetRule();
        }

        public IEnumerable<RuleList> GetRuleName(int no)
        {
            return Repository.GetRuleName(no);
        }
    }
}
