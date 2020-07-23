using System;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.RecordTrancation
{
    [Table("ZH_XDLZ_LoanCustOfEmp")]
    public class LoanManager
    {
        public string DepartmentCode { get; set; }

        public string ManagerCode { get; set; }

        public string ManagerName { get; set; }
    }
}
