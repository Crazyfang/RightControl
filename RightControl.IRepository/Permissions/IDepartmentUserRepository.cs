using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model;
using RightControl.Model.Permissions;

namespace RightControl.IRepository.InforSearch
{
    public interface IDepartmentUserRepository:IBaseRepository<DepartmentUserModel>
    {
        Department GetDepartmentByUserId(int userId);

        List<UserModel> GetDepartmentUser(string departmentCode);
    }
}
