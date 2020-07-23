using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IRepository;
using RightControl.IRepository.InforSearch;
using RightControl.IService.InforSearch;
using RightControl.Model;
using RightControl.Model.InforSearch;

namespace RightControl.Service.InforSearch
{
    public class InforSearchOperateService:BaseService<InforSearchOperate>, IInforSearchOperateService
    {
        public IInforSearchOperateRepository InforSearchOperateRepository { get; set; }

        public IUserRepository UserRepository { get; set; }

        public dynamic GetListByFilter(InforSearchOperate filter, PageInfo pageInfo)
        {
            var _where = " where 1=1";
            if (filter.OperaterId!=0)
            {
                _where += " and OperaterId=@OperaterId";
            }

            return GetListByFilter(filter, pageInfo, _where);
        }

        public dynamic GetPageMulTable(InforSearchOperate filter, PageInfo pageInfo)
        {
            string _orderBy = string.Empty;
            string where = string.Empty;
            if (!string.IsNullOrEmpty(pageInfo.field))
            {
                _orderBy = string.Format(" ORDER BY {0} {1}", pageInfo.field, pageInfo.order);
            }
            else
            {
                _orderBy = " ORDER BY a.CreateOn desc";
            }

            if (!string.IsNullOrEmpty(filter.OperaterName))
            {
                where = string.Format(" where b.RealName like '%{0}%'", filter.OperaterName);
            }
            long total = 0;
            var list = InforSearchOperateRepository.GetPageMulTable(new SearchFilter { pageIndex = pageInfo.page, pageSize = pageInfo.limit, returnFields = pageInfo.returnFields, param = filter, where = where, orderBy = _orderBy }, out total);

            return new { code = 0, count = total, data = list };

            //return InforSearchOperateRepository.GetPageMulTable(filter, out total);
        }
    }
}
