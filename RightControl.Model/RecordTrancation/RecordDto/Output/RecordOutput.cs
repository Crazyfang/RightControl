using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.RecordTrancation.RecordDto.Output
{
    public class RecordOutput
    {
        public string RecordID { get; set; }

        public string RecordUserName { get; set; }

        public string RecordUserInCode { get; set; }

        public string RecordUserCode { get; set; }

        public string RecordManagerDepartment { get; set; }

        public string DepartmentName { get; set; }

        public int RecordStatus { get; set; }

        public DateTime CreditDueDate { get; set; }

        public string RecordManager { get; set; }

        public string RealName { get; set; }

        public string RecordStorageLoc { get; set; }

        public int RecordType { get; set; }

        public DateTime CreateOn { get; set; }

        public DateTime? HandOverTime { get; set; }
    }
}
