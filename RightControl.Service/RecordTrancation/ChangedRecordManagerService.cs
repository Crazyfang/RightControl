using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class ChangedRecordManagerService:BaseService<ChangedRecordManager>, IChangedRecordManagerService
    {
        public dynamic GetListByFilter(ChangedRecordManager filter, PageInfo pageInfo)
        {
            var where = " where 1=1";
            pageInfo.field = "CustINNO";
            pageInfo.order = "asc";

            if (!string.IsNullOrEmpty(filter.CustINNO))
            {
                where += " and CustINNO=@CustINNO";
            }

            return GetListByFilter(filter, pageInfo, where);
        }
    }
}
