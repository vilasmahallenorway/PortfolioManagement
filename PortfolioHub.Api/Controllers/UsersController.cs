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
    public class UsersController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService, IMapper mapper, ILogger<UsersController> logger)
        {
            this._userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserResponseDto>> CreateUserAsync([FromBody] UserCreationDto createUserRequest)
        {
            _logger.LogInformation("Create User request started");

            var user = _mapper.Map<Users>(createUserRequest);

            Users createdUsersFromDb = await _userService.AddUserAsync(user);
            if (createdUsersFromDb is null)
            {
                _logger.LogWarning("Create user failed at {RequestTime}" + DateTime.Now);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var userResponse = _mapper.Map<UserResponseDto>(createdUsersFromDb);

            return CreatedAtAction(nameof(GetUserByIdAsync), new { id = userResponse.Id }, userResponse);
        }


        [HttpGet("{id}")]
        [ActionName(nameof(GetUserByIdAsync))]
        [ProducesResponseType(typeof(PlantResponseDto), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get Plant by Id", OperationId = "GetUserById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully Fetched the user for the given id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDto>> GetUserByIdAsync(Guid id)
        {
            var userFromDb = await _userService.GetUserByIdAsync(id);
            if (userFromDb is null)
            {
                return NotFound();
            }
            var userResponse = _mapper.Map<UserResponseDto>(userFromDb);

            return Ok(userResponse);
        }

        [HttpGet("GetAllUsers")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get all users", OperationId = "GetAllUsers")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully Fetched users")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDto>> GetAllUsersAsync()
        {
            var userFromDb = await _userService.GetAllUsersAsync();
            if (userFromDb.Count == 0)
            {
                return NotFound("No users exist");
            }
            var userResponse = _mapper.Map<List<UserResponseDto>>(userFromDb);

            return Ok(userResponse);
        }


        [HttpDelete("DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Delete User for provided user id", OperationId = "DeleteUserByUserIdAsync")]

        [SwaggerResponse(StatusCodes.Status200OK, "Removed the User for provided user Id")]
        public async Task<IActionResult> DeleteUserByUserIdAsync(Guid userId)
        {
            var userFromDb = await _userService.GetUserByIdAsync(userId);
            if (userFromDb is null)
            {
                return NotFound($"user does not exist for the user Id = {userId}");
            }

            await _userService.DeleteUserByUserIdAsync(userId);
            return NoContent();
        }
    }
}
