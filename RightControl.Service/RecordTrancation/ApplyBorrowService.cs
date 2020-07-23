using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IRepository.RecordTrancation;
using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class ApplyBorrowService:BaseService<ApplyBorrowTable>, IApplyBorrowService
    {
        public IApplyBorrowRepository repository { get; set; }
        public dynamic GetListByFilter(ApplyBorrowTable filter, PageInfo pageInfo)
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

        public bool ApplyBorrow(string borrowId, ApplyBorrowTable obj, List<string> fileList, string operateName)
        {
            return repository.BorrowRecord(borrowId, obj, fileList, operateName);
        }

        public bool ApplyBatchBorrow(string borrowId, ApplyBorrowTable obj, string departmentId, string operateName)
        {
            return repository.ApplyBatchBorrow(borrowId, obj, departmentId, operateName);
        }

        public bool ApplyBorrowAgree(string borrowId)
        {
            return repository.ApplyBorrowAgree(borrowId);
        }
    }
}
