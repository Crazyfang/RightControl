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
    public class DepartmentService:BaseService<DepartmentModel>, IDepartmentService
    {
        public IDepartmentRepository repository { get; set; }
        public List<DisplayTreeModel> GetTrees(string id)
        {
            return repository.GetTrees(id);
        }

        public dynamic GetListByFilter(DepartmentModel filter, PageInfo pageInfo)
        {
            string _where = " where 1=1";
            if (!string.IsNullOrEmpty(filter.DepartmentCode))
            {
                _where += " and DepartmentCode=@DepartmentCode";
            }
            if (filter.Status != null)
            {
                _where += " and Status=@Status";
            }
            return GetListByFilter(filter, pageInfo, _where);
        }

        public List<DepartmentModel> GetAllDepartment()
        {
            return repository.GetAllDepartments();
        }
    }
}
