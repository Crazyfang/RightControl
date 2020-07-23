using Dapper;
using RightControl.Common;
using RightControl.IRepository;
using RightControl.IRepository.InforSearch;
using RightControl.Model;
using RightControl.Model.Permissions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RightControl.Repository.Permissions
{
    public class DeparmentRepository: BaseRepository<DepartmentModel>, IDepartmentRepository
    {
        public List<DisplayTreeModel> GetTrees(string id)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                //StringBuilder sb = new StringBuilder();
                //bool sign = false;
                //if (string.IsNullOrEmpty(id))
                //{
                //    sb.Append("SELECT * from t_department where ParentId=0;");
                //}
                //else
                //{
                //    sb.Append("SELECT * from t_department where ParentId=@ParentId;");
                //    sign = true;
                //}

                var all = conn.Query<DepartmentModel>("SELECT * from t_department");

                var data = string.IsNullOrEmpty(id)
                    ? all.Where(item => item.ParentId == 0)
                    : all.Where(item => item.ParentId == int.Parse(id));
  
                //var data = sign?conn.Query<DepartmentModel>(sb.ToString(), new {ParentId=id}):conn.Query<DepartmentModel>(sb.ToString());
                var test = data.Select(x => new DisplayTreeModel()
                {
                    id = x.Id,
                    name = x.DepartmentName,
                    pId = x.ParentId.ToString(),
                    isParent = all.Any(c => c.ParentId == x.Id)
                }).ToList();

                return test;
            }
        }

        public List<DepartmentModel> GetAllDepartments()
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                var all = conn.Query<DepartmentModel>("SELECT * from t_department where Status=1"); 
                return all.ToList();
            }
        }
    }
}
