using RightControl.IRepository.RecordSetting;
using RightControl.IService.RecordSetting;
using RightControl.Model;
using RightControl.Model.RecordSetting;

namespace RightControl.Service
{
    public class SelectTypeNameService:BaseService<SelectTypeName>, ISelectTypeNameService
    {
        public ISelectTypeNameRepository SelectTypeNameRepository { get; set; }

        public dynamic GetListByFilter(SelectTypeName filter, PageInfo pageInfo)
        {
            string _where = " where 1=1";
            if (string.IsNullOrEmpty(filter.SelectTypeNameString))
            {
                _where += $" and SelectTypeNameString={filter.SelectTypeNameString}";
            }

            return GetListByFilter(filter, pageInfo, _where);
        }

        public int CreateModelReturnId(SelectTypeName model)
        {
            return SelectTypeNameRepository.Create(model);
        }

        public bool DeleteSelectTypeName(int id)
        {
            return SelectTypeNameRepository.DeleteSelectTypeName(id);
        }
    }
}
