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
    public class TenantsController : BaseController
    {
        private readonly ILogger<TenantsController> _logger;
        private readonly IMapper _mapper;
        private readonly ITenantsService _tenantsService;

        public TenantsController(ITenantsService tenantService, IMapper mapper, ILogger<TenantsController> logger)
        {
            this._tenantsService = tenantService ?? throw new ArgumentNullException(nameof(tenantService));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TenantResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TenantResponseDto>> CreateTenantAsync([FromBody] TenentCreationDto createTenantRequest)
        {
            //ToDo - Fluent Validation to verify request object properties 

            var tenant = _mapper.Map<Tenants>(createTenantRequest);

            Tenants createdTenantFromDb = await _tenantsService.AddTenantAsync(tenant);
            if (createdTenantFromDb is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var tenantResponse = _mapper.Map<TenantResponseDto>(createdTenantFromDb);

            return CreatedAtAction(nameof(GetTenantByIdAsync), new { id = tenantResponse.Id }, tenantResponse);
        }


        [HttpGet("{id}")]
        [ActionName(nameof(GetTenantByIdAsync))]
        [ProducesResponseType(typeof(TenantResponseDto), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get Tenant by Id", OperationId = "GetTenantById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully Fetched the tenant for the given id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TenantResponseDto>> GetTenantByIdAsync(Guid id)
        {
            var tenantFromDb = await _tenantsService.GetTenantByIdAsync(id);
            if (tenantFromDb is null)
            {
                return NotFound();
            }
            var tenantResponse = _mapper.Map<TenantResponseDto>(tenantFromDb);

            return Ok(tenantResponse);
        }

        [HttpGet("GetAllTenants")]
        [ProducesResponseType(typeof(TenantResponseDto), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get all Tenants", OperationId = "GetAllTenants")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully Fetched the tenants")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TenantResponseDto>> GetAllTenantsAsync()
        {
            var tenantsFromDb = await _tenantsService.GetAllTenantsAsync();
            if (tenantsFromDb.Count == 0)
            {
                return NotFound("No Tenant record exist in system");
            }
            var tenantResponse = _mapper.Map<List<TenantResponseDto>>(tenantsFromDb);

            return Ok(tenantResponse);
        }


        [HttpPut("UpdateTenant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully updated tenant")]
        public async Task<ActionResult<TenantResponseDto>> UpdateTenantAsync(TenantUpdationDto tenantUpdationDto)
        {
            //ToDo - Fluent Validation to verify request object properties 

            var tenant = await _tenantsService.GetTenantByIdAsync(tenantUpdationDto.Id);

            if (tenant == null)
            {
                throw new Exception($"tenant Id {tenantUpdationDto.Id} is not found.");
            }

            var tenantResponse = _mapper.Map(tenantUpdationDto, tenant);

            await _tenantsService.UpdateTenantAsync(tenantResponse);

            return CreatedAtAction(nameof(GetTenantByIdAsync), new { id = tenantUpdationDto.Id }, tenantResponse);
        }


        [HttpDelete("DeleteTenant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Delete Tenant", OperationId = "DeleteTenantAsync")]

        [SwaggerResponse(StatusCodes.Status200OK, "Removed the Tenant for given tenant Id")]
        public async Task<IActionResult> DeleteTenantAsync(Guid tenantId)
        {
            var tenantFromDb = await _tenantsService.GetTenantByIdAsync(tenantId);
            if (tenantFromDb is null)
            {
                return NotFound($"Tenant with Id = {tenantId} not found");
            }

            await _tenantsService.DeleteTenantAsync(tenantId);
            return NoContent();
        }

    }
}
