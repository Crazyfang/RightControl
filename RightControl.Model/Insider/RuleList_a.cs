using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DapperExtensions;

namespace RightControl.Model.Insider
{
    public class RuleList_a
    {
        public int R_No { get; set; }
        public string R_Name { get; set; }

        public RuleList friend { get; set; }

    }
}
