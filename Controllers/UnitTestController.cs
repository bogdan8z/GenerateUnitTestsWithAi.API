using GenerateUnitTestsWithAi.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GenerateUnitTestsWithAi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnitTestController : ControllerBase
    {   
        private readonly ILogger<UnitTestController> _logger;
        private readonly IUnitTestService _unitTestService;

        public UnitTestController(ILogger<UnitTestController> logger, IUnitTestService unitTestService)
        {
            _logger = logger;          
            _unitTestService = unitTestService;
        }

        [HttpGet]
        public async Task<IActionResult> GenerateUnitTests(string method)
        {            
            method = "public bool IsOneTwoThree(string str){return str==\"123\";}";
            var response = await _unitTestService.GenerateUnitTest(method);
        
            _logger.LogInformation("GenerateUnitTests done");
            return Ok(response);                  
        }

    }
}
