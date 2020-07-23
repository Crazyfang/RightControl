using AutoMapper;
using RightControl.Common;
using RightControl.IRepository;
using RightControl.IService;
using RightControl.Model;
using RightControl.Model.Permissions;

namespace RightControl.Service
{
    public class UserService : BaseService<UserModel>, IUserService
    {
        public IUserRepository repository;
        public IFreeSql fsql;
        public UserService(IUserRepository userRepository, IFreeSql freeSql)
        {
            repository = userRepository;
            fsql = freeSql;
        }
        public UserModel GetDetail(int Id)
        {
            return repository.GetDetail(Id);
        }
        public UserModel CheckLogin(string username, string password)
        {
            return repository.CheckLogin(username, password);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model">密码实体</param>
        /// <returns></returns>
        public bool ModifyPwd(PassWordModel model)
        {
            model.OldPassword = Md5.md5(model.OldPassword, 32);
            model.Password = Md5.md5(model.Password, 32);
            return repository.ModifyPwd(model) > 0 ? true : false;
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InitPwd(UserModel model)
        {
            return repository.Update(model, "PassWord") > 0 ? true : false;
        }

        public dynamic GetListByFilter(UserModel filter, PageInfo pageInfo)
        {
            pageInfo.prefix = "u.";
            string _where = " t_User u INNER JOIN t_role r on u.RoleId=r.Id LEFT JOIN t_DepartmentUser du on u.Id=du.UserId LEFT JOIN t_department d on d.Id=du.DepartmentId where 1=1";
            if (!string.IsNullOrEmpty(filter.UserName))
            {
                _where += string.Format(" and {0}UserName=@UserName", pageInfo.prefix);
            }
            if (filter.RoleId != 0)
            {
                _where += string.Format(" and {0}RoleId=@RoleId", pageInfo.prefix);
            }
            if (filter.Status != null)
            {
                _where += string.Format(" and {0}Status=@Status", pageInfo.prefix);
            }
            if (!string.IsNullOrEmpty(pageInfo.field))
            {
                pageInfo.field = pageInfo.prefix + pageInfo.field;
            }
            pageInfo.returnFields = string.Format("{0}Id,{0}UserName,{0}RealName,{0}CreateOn,{0}PassWord,{0}Status,{0}RoleId,r.RoleName,d.DepartmentName", pageInfo.prefix);
            return GetPageUnite(baseRepository, pageInfo, _where, filter);
        }

        public int CreateModelReturnId(UserModel model)
        {
            return repository.Create(model);
        }

        public dynamic GetUserByDepartmentCode(int departmentId)
        {
            var data = fsql.Select<DepartmentModel>()
                .Where(i => i.Id == departmentId)
                .IncludeMany(i => i.UserList)
                .ToOne();

            return data.UserList;
        }

        public dynamic GetUserByUserName(string userId)
        {
            var user = fsql.Select<UserModel>()
                .Where(i => i.UserName == userId)
                .ToOne();

            return user;
        }
    }
}
