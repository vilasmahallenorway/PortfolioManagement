using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioHub.Api.Contracts.Request;
using PortfolioHub.Api.Contracts.Response;
using PortfolioHub.Application.Contracts;
using PortfolioHub.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PortfolioHub.Api.Controllers
{
    [Route("api/[controller]")]
    public class PlantsController : BaseController
    {
        private readonly ILogger<PlantsController> _logger;
        private readonly IMapper _mapper;
        private readonly IPlantsService _plantService;

        public PlantsController(IPlantsService plantService, IMapper mapper, ILogger<PlantsController> logger)
        {
            this._plantService = plantService ?? throw new ArgumentNullException(nameof(plantService));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [ProducesResponseType(typeof(PlantResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlantResponseDto>> CreatePlantAsync([FromBody] PlantCreationDto createPlantRequest)
        {
            _logger.LogInformation("Create Plant request started");

            var plant = _mapper.Map<Plants>(createPlantRequest);

            // ToDo - verify first that provided portfolio Id should be exist in Portfolio table, If not donot proceed further 

            Plants createdPlantFromDb = await _plantService.AddPlantsAsync(plant);
            if (createdPlantFromDb is null)
            {
                _logger.LogWarning("Create Plant failed at {RequestTime}" + DateTime.Now);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var plantResponse = _mapper.Map<PlantResponseDto>(createdPlantFromDb);

            return CreatedAtAction(nameof(GetPlantByIdAsync), new { id = plantResponse.Id }, plantResponse);
        }


        [HttpGet("{id}")]
        [ActionName(nameof(GetPlantByIdAsync))]
        [ProducesResponseType(typeof(PlantResponseDto), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get Plant by Id", OperationId = "GetPlantById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully Fetched the portfolio for the given id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlantResponseDto>> GetPlantByIdAsync(Guid id)
        {
            var plantFromDb = await _plantService.GetPlantByIdAsync(id);
            if (plantFromDb is null)
            {
                return NotFound();
            }
            var plantResponse = _mapper.Map<PlantResponseDto>(plantFromDb);

            return Ok(plantResponse);
        }

        [HttpGet("GetAllPlantsForPortfolio")]
        [ProducesResponseType(typeof(PlantResponseDto), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get all Plants for the provided portfolio Id", OperationId = "GetAllPlantsForPortfolio")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully Fetched the plant for provided portfolio id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlantResponseDto>> GetAllPlantsForPortfolioAsync(Guid portfolioId)
        {
            var plantFromDb = await _plantService.GetAllPlantsByPortfolioIdAsync(portfolioId);
            if (plantFromDb.Count == 0)
            {
                return NotFound("No plants record found for provided portfolio Id");
            }
            var plantResponse = _mapper.Map<List<PlantResponseDto>>(plantFromDb);

            return Ok(plantResponse);
        }


        [HttpDelete("DeletePlantForPortfolio")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Delete Plant for provided portfolio", OperationId = "DeletePlantForPortfolioAsync")]

        [SwaggerResponse(StatusCodes.Status200OK, "Removed the Plant for provided portfolio Id")]
        public async Task<IActionResult> DeletePlantForPortfolioAsync(Guid portfolioId)
        {
            var plantsFromDb = await _plantService.GetAllPlantsByPortfolioIdAsync(portfolioId);
            if (plantsFromDb is null || plantsFromDb.Count <= 0)
            {
                return NotFound($"Plants does not exist for the Portfolio Id = {portfolioId}");
            }

            await _plantService.DeletePlantsByPortfolioIdAsync(portfolioId);
            return NoContent();
        }
    }
}
