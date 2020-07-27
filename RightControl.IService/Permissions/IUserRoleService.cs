using System.Collections.Generic;

namespace RightControl.IService.Permissions
{
    public interface IUserRoleService
    {
        bool UpdateUserRoleRelation(string roleStr, string userName);

        List<int> GetUserRoleList(string userName);

        bool InsertUserRoleRelation(string roleStr, string userName);
    }
}
