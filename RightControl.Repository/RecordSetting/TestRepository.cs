using FreeSql;
using RightControl.IRepository.RecordSetting;
using RightControl.Model.RecordSetting;

namespace RightControl.Repository.RecordSetting
{
    public class TestRepository:RepositoryBase<Test>, ITestRepository
    {
        public TestRepository(IFreeSql fsql) : base(fsql)
        {
        }
    }
}
