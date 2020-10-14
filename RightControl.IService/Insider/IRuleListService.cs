using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.Insider;

namespace RightControl.IService.Insider
{
    public interface IRuleListService : IBaseService<XD_RuleList>
    {
        IEnumerable<XD_RuleList> GetRule();

        //根据规则编号得到规则名称
        IEnumerable<XD_RuleList> GetRuleName(int no);
    }
}
