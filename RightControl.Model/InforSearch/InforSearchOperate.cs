using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;

namespace RightControl.Model.InforSearch
{
    public class InforSearchOperate
    {
        [DapperExtensions.Key(true)]
        public int Id { get; set; }

        public int OperaterId { get; set; }

        public DateTime CreateOn { get; set; }

        public string OperateMsg { get; set; }

        [Computed]
        public string OperaterName { get; set; }

        [Computed]
        public string DepartmentName { get; set; }
    }
}
