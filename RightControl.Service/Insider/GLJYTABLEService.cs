using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IRepository;
using RightControl.IRepository.Insider;
using RightControl.IService.Insider;
using RightControl.Model;
using RightControl.Model.Insider;

namespace RightControl.Service.Insider
{
    public class GLJYTABLEService : BaseService<YG_GLJYTABLE>, IGLJYTABLEService
    {
        public IGLJYTABLERepository GLJYTABLERepository { get; set; }
        public IUserRepository userRepository { get; set; }

        public dynamic GetListByFilter(YG_GLJYTABLE filter, PageInfo pageInfo)
        {
            pageInfo.field = "CERTTYPE";
            pageInfo.order = "ASC";
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
