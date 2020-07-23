using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.RecordTrancation
{
    public class ExpiredClass
    {
        public List<RecordList> recordList { get; set; }

        public List<OtherFileList> otherList { get; set; }

        public List<ExpiredFileVerifyEntity> expiredVerifyList { get; set; }
    }
}
