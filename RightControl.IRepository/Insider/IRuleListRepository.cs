using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.Insider;

namespace RightControl.IRepository.Insider
{
    public interface IRuleListRepository : IBaseRepository<RuleList>
    {
        IEnumerable<RuleList> GetRule();

        IEnumerable<RuleList> GetRuleName(int no);
    }
}
