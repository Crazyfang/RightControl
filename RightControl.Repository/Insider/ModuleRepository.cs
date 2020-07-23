using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IRepository.Insider;
using RightControl.Model.Insider;
using Dapper;

namespace RightControl.Repository.Insider
{
    public class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Module> GetModule()
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var sql = "SELECT No,Name FROM Module";
                return conn.Query<Module>(sql);
            }
        }
    }
}
