using AutoMapper;
using GenerateUnitTestsWithAi.API.Models;
using GenerateUnitTestsWithAi.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GenerateUnitTestsWithAi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransformationController : ControllerBase
    {
        private readonly ILogger<TransformationController> _logger;
        private readonly ITransformationService _transformationService;
        private readonly IMapper _mapper;

        public TransformationController(
            ILogger<TransformationController> logger, 
            ITransformationService transformationService,
            IMapper mapper)
        {
            _logger = logger;
            _transformationService = transformationService;  
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddToDictionary([FromBody] AddTransformationModel transformationModel)
        {
            if (transformationModel == null)
            {
                return BadRequest();
            }

            var result = _transformationService.WriteTransformation(transformationModel.Key);

            if (!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

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

        [HttpPost("TransformCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult TransformCode([FromBody] DoTransformationModel doTransformationModel)
        {

            var result = _transformationService.TransformCode(doTransformationModel.Code);

            _logger.LogInformation("TransformCode done");
            return Ok(result);
        }
    }
}
