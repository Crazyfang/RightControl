using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IRepository;
using RightControl.IRepository.PfLender;
using RightControl.IService.PfLender;
using RightControl.Model;
using RightControl.Model.PfLender;

namespace RightControl.Service.PfLender
{
    public class LenderListDepFlowService : BaseService<LenderList_DepFlow>, ILenderListDepFlowService
    {
        public ILenderListDepFlowRepository LenderListDepFlowRepository { get; set; }
        public IUserRepository userRepository { get; set; }

        public dynamic GetListByFilter(LenderList_DepFlow filter, PageInfo pageInfo)
        {
            pageInfo.field = "BJ01DATE DESC,";
            pageInfo.order = "CUST_CSNO ASC";
            var _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.BJ01DATE))
            {
                _where += string.Format(" and BJ01DATE = '{0}'", filter.BJ01DATE);
            }
            else
            {
                _where += " and BJ01DATE = convert(char(10),getdate()-1,120)";
            }

            return GetListByFilter(filter, pageInfo, _where);
        }
    }
}
