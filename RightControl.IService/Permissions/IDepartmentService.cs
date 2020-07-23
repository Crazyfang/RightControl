using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.Permissions;

namespace RightControl.IService.Permissions
{
    public interface IDepartmentService:IBaseService<DepartmentModel>
    {
        List<DisplayTreeModel> GetTrees(string id);

        List<DepartmentModel> GetAllDepartment();
    }
}
