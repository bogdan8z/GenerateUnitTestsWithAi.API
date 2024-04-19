using AutoMapper;
using GenerateUnitTestsWithAi.API.Models;
using GenerateUnitTestsWithAi.API.Services;
using GenerateUnitTestsWithAi.API.Services.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System.Runtime;

namespace GenerateUnitTestsWithAi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransformationController : ControllerBase
    {
        private readonly ILogger<TransformationController> _logger;
        private readonly ITransformationService _transformationService;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _settings;

        public TransformationController(
            ILogger<TransformationController> logger,
            ITransformationService transformationService,
            IMapper mapper,
            IOptions<AppSettings> settings)
        {
            _logger = logger;
            _transformationService = transformationService;
            _mapper = mapper;
            _settings = settings;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddToDictionary([FromBody] string code)
        {
            if (code == null)
            {
                return BadRequest();
            }

            _transformationService.WriteTransformation(code);

            _logger.LogInformation("AddToDictionary done");
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TransformationGetModel), StatusCodes.Status200OK)]

        public IActionResult GetAll()
        {

            var result = _transformationService.GetAllTransformation();

            var returnModel = _mapper.Map<List<TransformationGetModel>>(result);

            _logger.LogInformation("GetAll done");
            return Ok(returnModel);
        }

        [HttpPost("Encode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult TransformEncode([FromBody] string code)
        {
            if (code == null)
            {
                return BadRequest();
            }

            var result = _transformationService.TransformEncode(code);

            _logger.LogInformation("TransformCode done");
            return Ok(result);
        }

        [HttpPost("Decode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult TransformDecode([FromBody] string code)
        {
            if (code == null)
            {
                return BadRequest();
            }

            var result = _transformationService.TransformDecode(code);

            _logger.LogInformation("TransformCode done");
            return Ok(result);
        }

        [HttpGet("ReadMethodFromFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ReadMethodFromFile()
        {
            var filePath = _settings.Value.FormatTextPath;

            if (!System.IO.File.Exists(filePath))
            {
                return BadRequest();
            }

            var res = System.IO.File.ReadAllText(filePath);
            res = "\"" + FormatMe(res) + "\"";
            return Ok(res);
        }

        private static string FormatMe(string input) => input.Replace(Environment.NewLine, " ").Replace("\"", "\\\"");
    }
}