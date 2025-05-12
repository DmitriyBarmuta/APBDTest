using Test.Services;
using Tutorial9.Repository;

namespace Tutorial9.Services;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepository;

    public TestService(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    
}