using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.Permissions;

namespace RightControl.IRepository.InforSearch
{
    public interface IDepartmentRepository:IBaseRepository<DepartmentModel>
    {
        List<DisplayTreeModel> GetTrees(string id);

        List<DepartmentModel> GetAllDepartments();
    }
}
