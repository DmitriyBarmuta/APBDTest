using Microsoft.AspNetCore.Mvc;
using Test.Services;
using Tutorial9.Services;

namespace Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ITestService _testService;

    public TestController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpGet]
    public async Task<IActionResult> TestGet(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}