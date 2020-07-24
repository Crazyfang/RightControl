using RightControl.IService.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Service.Permissions
{
    public class UserRoleService:IUserRoleService
    {
        private readonly IFreeSql fsql;

        public UserRoleService(IFreeSql freesql)
        {
            fsql = freesql;
        }
    }
}
