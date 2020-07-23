using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.RecordTrancation
{
    public class GetNum
    {
        [DapperExtensions.Key(true)]
        public int Id { get; set; }

        public int Msg { get; set; }
    }
}
