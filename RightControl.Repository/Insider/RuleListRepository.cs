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
    public class RuleListRepository : BaseRepository<XD_RuleList>, IRuleListRepository
    {
        /// <summary>
        /// 获取规则列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<XD_RuleList> GetRule()
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var sql = "SELECT R_No,R_Name FROM XD_RuleList";
                return conn.Query<XD_RuleList>(sql);
            }
        }

        /// <summary>
        /// 根据规则编号获取规则名称
        /// </summary>
        /// <param name="no">规则编号</param>
        /// <returns></returns>
        public IEnumerable<XD_RuleList> GetRuleName(int no)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var sql = "SELECT R_No,R_Name FROM XD_RuleList Where R_No=" + no;
                return conn.Query<XD_RuleList>(sql);
            }
        }
    }
}
