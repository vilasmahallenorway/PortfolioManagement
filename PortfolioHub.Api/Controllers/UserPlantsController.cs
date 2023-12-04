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
    public class UserPlantsController : BaseController
    {
        private readonly ILogger<UserPlantsController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserPlantsService _userPlantService;

        public UserPlantsController(IUserPlantsService userPlantService, IMapper mapper, ILogger<UserPlantsController> logger)
        {
            this._userPlantService = userPlantService ?? throw new ArgumentNullException(nameof(userPlantService));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserPlantsResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserPlantsResponseDto>> CreatePlantAsync([FromBody] UserPlantsCreationDto createUserPlantsRequest)
        {
            _logger.LogInformation("Create user Plants request started");

            //ToDo - Fluent Validation to verify request object properties 

            var userPlant = _mapper.Map<UserPlants>(createUserPlantsRequest);

            // ToDo - verify if a combination of userId and plantId exists in a database before proceeding further,
            // If exist do not proceed further

            UserPlantsResponseDto createdUserPlantFromDb = await _userPlantService.AddUserPlantAsync(userPlant);
            if (createdUserPlantFromDb is null)
            {
                _logger.LogWarning("Create User Plant failed at {RequestTime}" + DateTime.Now);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            _logger.LogInformation("User Plant creted Successfully");

            return createdUserPlantFromDb;
        }


        [HttpGet("{id}")]
        [ActionName(nameof(GetUserPlantByIdAsync))]
        [ProducesResponseType(typeof(UserPlantsResponseDto), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get User Plant by Id", OperationId = "GetUserPlantById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully Fetched the user plants for the given id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserPlantsResponseDto>> GetUserPlantByIdAsync(Guid id)
        {
            var userPlantsFromDb = await _userPlantService.GetUserPlantsByIdAsync(id);
            if (userPlantsFromDb is null)
            {
                return NotFound();
            }

            return Ok(userPlantsFromDb);
        }

        [HttpGet("GetUserPlantsByUserId")]
        [ProducesResponseType(typeof(UserPlantsResponseDto), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get all Plants for the provided user Id", OperationId = "GetUserPlantsByUserId")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully Fetched the user plant for provided user id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserPlantsResponseDto>> GetUserPlantsByUserIdAsync(Guid userId)
        {
            var userPlantFromDb = await _userPlantService.GetUserPlantsByUserIdAsync(userId);
            if (userPlantFromDb == null)
            {
                return NotFound("No plants record found for provided user Id");
            }

            return Ok(userPlantFromDb);
        }


        [HttpDelete("DeleteUserPlantsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Delete User Plant record by Id", OperationId = "DeleteUserPlantsById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Removed the User Plant record for provided Id")]
        public async Task<IActionResult> DeleteUserPlantByIdAsync(Guid id)
        {
            // ToDo: Validation on input

            await _userPlantService.DeleteUserPlantsByIdAsync(id);
            return NoContent();
        }
    }
}
