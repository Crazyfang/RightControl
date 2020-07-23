using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IRepository.InforSearch;
using RightControl.IService.Permissions;
using RightControl.Model;
using RightControl.Model.Permissions;

namespace RightControl.Service.Permissions
{
    public class DepartmentUserService:BaseService<DepartmentUserModel>, IDepartmentUserService
    {
        public IDepartmentUserRepository repository { get; set; }
        public dynamic GetListByFilter(DepartmentUserModel filter, PageInfo pageInfo)
        {
            string _where = " where 1=1";
            if (filter.Id != 0)
            {
                _where += " and Id=@Id";
            }
            return GetListByFilter(filter, pageInfo, _where);
        }

        public Department GetDepartmentByUserId(int userId)
        {
            return repository.GetDepartmentByUserId(userId);
        }

        public List<UserModel> GetDepartmentUser(string departmentCode)
        {
            return repository.GetDepartmentUser(departmentCode);
        }
    }
}
