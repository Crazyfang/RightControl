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
    public class RuleListService : BaseService<XD_RuleList>, IRuleListService
    {
        public IRuleListRepository Repository { get; set; }
        public IUserRepository userRepository { get; set; }

        public dynamic GetListByFilter(XD_RuleList filter, PageInfo pageInfo)
        {
            var _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.R_Name))
            {
                _where += string.Format(" and R_Name like '%{0}%'", filter.R_Name);
            }

            return GetListByFilter(filter, pageInfo, _where);
        }

        public IEnumerable<XD_RuleList> GetRule()
        {
            return Repository.GetRule();
        }

        public IEnumerable<XD_RuleList> GetRuleName(int no)
        {
            return Repository.GetRuleName(no);
        }
    }
}
