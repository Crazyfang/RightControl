using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using RightControl.IRepository.InforSearch;
using RightControl.Model;
using RightControl.Model.Permissions;

namespace RightControl.Repository.Permissions
{
    public class DepartmentUserRepository:BaseRepository<DepartmentUserModel>, IDepartmentUserRepository
    {
        public Department GetDepartmentByUserId(int userId)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                string sql = "select b.Id as DepartmentId, b.DepartmentName, b.DepartmentCode from t_DepartmentUser a left join t_department b on a.DepartmentId = b.Id where a.UserId = @UserId";
                return conn.Query<Department>(sql, new { UserId=userId }).FirstOrDefault();
            }
        }

        public List<UserModel> GetDepartmentUser(string departmentCode)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                string sql = "  select a.*, c.DepartmentCode as DepartmentCode, c.DepartmentName as DepartmentName from t_user a left join t_DepartmentUser b on a.Id = b.UserId"+
                " left join t_department c on b.DepartmentId=c.Id where c.DepartmentCode=@DepartmentCode";
                return conn.Query<UserModel>(sql, new { DepartmentCode = departmentCode }).ToList();
            }
        }
    }
}
