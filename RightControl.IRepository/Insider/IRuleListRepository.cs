﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.Insider;

namespace RightControl.IRepository.Insider
{
    public interface IRuleListRepository : IBaseRepository<XD_RuleList>
    {
        IEnumerable<XD_RuleList> GetRule();

        IEnumerable<XD_RuleList> GetRuleName(int no);
    }
}
