using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.IRepository.RecordTrancation;
using RightControl.IService;
using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.Service.RecordTrancation
{
    public class SelectTypeService:BaseService<SelectType>, ISelectTypeService
    {
        public ISelectTypeRepository repository { get; set; }
        public dynamic GetListByFilter(SelectType filter, PageInfo pageInfo)
        {
            string _where = " where 1=1";
            if (filter.SelectTypeNum != 0)
            {
                _where += " and SelectTypeNum=@SelectTypeNum";
            }

            return GetListByFilter(filter, pageInfo, _where);
        }

        public List<SelectType> GetSelectTypeName(int recordTypeId)
        {
            return repository.GetSelectTypeName(recordTypeId);
        }
    }
}
