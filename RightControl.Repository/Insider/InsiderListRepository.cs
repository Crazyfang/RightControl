using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IRepository.Insider;
using RightControl.Model.Insider;
using System.Data;
using Dapper;


namespace RightControl.Repository.Insider
{
    public class InsiderListRepository : BaseRepository<YG_InsiderList>, IInsiderListRepository
    {
        /// <summary>
        /// 获取对应岗位的内部人列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetInsiderList(string InsiderPs)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var sql = "select Id,RepartyNm from YG_InsiderList where Post = '" + InsiderPs + "'";
                return conn.Query<YG_InsiderList>(sql).ToList();
            }
        }

        /// <summary>
        /// 获取所有的关联方岗位
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetAllPost()
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var sql = "select distinct Post from YG_InsiderList";
                return conn.Query<YG_InsiderList>(sql).ToList();
            }
        }
    }
}
