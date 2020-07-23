using RightControl.IRepository.RecordTrancation;
using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;
using System.Collections.Generic;

namespace RightControl.Service.RecordTrancation
{
    public class ApplyCopyService:BaseService<ApplyCopyTable>, IApplyCopyService
    {
        public IApplyCopyRepository Repository { get; set; }
        public dynamic GetListByFilter(ApplyCopyTable filter, PageInfo pageInfo)
        {
            pageInfo.field = "ApplyTime";
            pageInfo.order = "asc";
            string where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.ApplyUser))
            {
                where += " and ApplyUser=@ApplyUser";
            }

            if (filter.ApplyState != 0)
            {
                where += " and ApplyState=@ApplyState";
            }

            return GetListByFilter(filter, pageInfo, where);
        }

        public bool CopyRecord(string borrowId, ApplyCopyTable obj, List<string> fileList)
        {
            return Repository.CopyRecord(borrowId, obj, fileList);
        }
    }
}
