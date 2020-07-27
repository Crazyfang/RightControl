using RightControl.Common;
using RightControl.IService.Permissions;
using RightControl.Model;
using RightControl.Model.Permissions;
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

        public bool UpdateUserRoleRelation(string roleStr, string userName)
        {
            var user = fsql.Select<UserModel>().Where(i => i.UserName == userName).First();
            var roleList = Array.ConvertAll(roleStr.Split(','), int.Parse).ToList();

            foreach(var item in roleList)
            {
                var result = fsql.Select<UserRoleModel>().Where(i => i.UserId == user.Id && i.RoleId == item).First();

                if (result == null)
                {
                    var obj = new UserRoleModel()
                    {
                        RoleId = item,
                        UserId = user.Id
                    };
                    var index = fsql.Insert(obj).ExecuteAffrows();
                }
            }

            fsql.Delete<UserRoleModel>().Where(i => i.UserId == user.Id && !roleList.Contains(i.RoleId));

            return true;
        }

        public List<int> GetUserRoleList(string userName)
        {
            var user = fsql.Select<UserModel>().Where(i => i.UserName == userName).First();
            if(user == null)
            {
                return new List<int>();
            }
            var result = fsql.Select<UserRoleModel>().Where(i => i.UserId == user.Id).ToList(i => i.RoleId);
            return result;
        }

        public bool InsertUserRoleRelation(string roleStr, string userName)
        {
            var user = fsql.Select<UserModel>().Where(i => i.UserName == userName).First();
            var roleList = Array.ConvertAll(roleStr.Split(','), int.Parse).ToList();

            foreach(var item in roleList)
            {
                var obj = new UserRoleModel()
                {
                    RoleId = item,
                    UserId = user.Id
                };
                var index = fsql.Insert(obj).ExecuteAffrows();
            }

            return true;
        }
    }
}
