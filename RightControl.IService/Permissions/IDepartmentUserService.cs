using RightControl.Model;
using RightControl.Model.Permissions;
using System.Collections.Generic;

namespace RightControl.IService.Permissions
{
    public interface IDepartmentUserService:IBaseService<DepartmentUserModel>
    {
        Department GetDepartmentByUserId(int userId);

        List<UserModel> GetDepartmentUser(string departmentCode);
    }
}
