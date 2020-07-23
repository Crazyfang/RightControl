using RightControl.IRepository.RecordSetting;
using RightControl.IService.RecordSetting;
using System.Threading.Tasks;
using RightControl.Model;
using RightControl.Model.RecordSetting;

namespace RightControl.Service.RecordSetting
{
    public class TestService:ITestService
    {
        private readonly ITestRepository _testRepository;
        public TestService(ITestRepository moduleRepository)
        {
            _testRepository = moduleRepository;
        }

        public async Task<IResponseOutput> GetAsync(long id)
        {
            _testRepository.Insert(new Test()
            {
                Rating = 6,
                Url = "我是谁"
            });
            var results = _testRepository.Select.WhereIf(true, c => c.Url == "321").ToList();
            var result = await _testRepository.GetAsync<Test>(id);
            return ResponseOutput.Ok(result);
        }
    }
}
