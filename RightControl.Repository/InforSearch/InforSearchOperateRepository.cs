using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using RightControl.IRepository.InforSearch;
using RightControl.Model;
using RightControl.Model.InforSearch;
using DapperExtensions.SqlServerExt;

namespace RightControl.Repository.InforSearch
{
    public class InforSearchOperateRepository:BaseRepository<InforSearchOperate>, IInforSearchOperateRepository
    {
        public IEnumerable<InforSearchOperate> GetPageMulTable(SearchFilter filter, out long total)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                return conn.GetByPageInforSearch<InforSearchOperate>(typeof(InforSearchOperate), filter.pageIndex, filter.pageSize, out total, filter.returnFields, filter.where, filter.param, filter.orderBy, filter.transaction, filter.commandTimeout);
            }
        }
    }
}
