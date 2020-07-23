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
    public class LenderListDepService:BaseService<LenderList_Dep>, ILenderListDepService
    {
        public ILenderListDepRepository LenderListDepRepository { get; set; }
        public IUserRepository userRepository { get; set; }

        public dynamic GetListByFilter(LenderList_Dep filter, PageInfo pageInfo)
        {
            pageInfo.field = "DATA_DATE DESC,";
            pageInfo.order = "CUST_CSNO ASC";
            var _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.DATA_DATE))
            {
                _where += string.Format(" and DATA_DATE = '{0}'", filter.DATA_DATE);
            }
            else
            {
                _where += " and DATA_DATE = convert(char(10),getdate()-1,120)";
            }

            return GetListByFilter(filter, pageInfo, _where);
        }
    }
}
