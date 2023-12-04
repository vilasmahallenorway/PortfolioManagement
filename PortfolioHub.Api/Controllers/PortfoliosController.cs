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
   
    public class PortfoliosController : BaseController
    {
        private readonly ILogger<PortfoliosController> _logger;
        private readonly IMapper _mapper;
        private readonly IPortfoliosService _portfolioService;

        public PortfoliosController(IPortfoliosService portfolioService, IMapper mapper, ILogger<PortfoliosController> logger)
        {
            this._portfolioService = portfolioService ?? throw new ArgumentNullException(nameof(portfolioService));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [ProducesResponseType(typeof(PortfolioResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PortfolioResponseDto>> CreatePortfolioAsync([FromBody] PortfolioCreationDto createPortfolioRequest)
        {
            _logger.LogInformation("Create Portfolios");

            var portfolio = _mapper.Map<Portfolios>(createPortfolioRequest);
            
            // ToDo - verify first that provided tenantId Id should be exist in Tanant table, If not donot proceed further

            Portfolios createdPortfolioFromDb = await _portfolioService.AddPortfolioAsync(portfolio);
            if (createdPortfolioFromDb is null)
            {
                _logger.LogWarning("Create Portfolio failed at {RequestTime}" + DateTime.Now);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var portfolioResponse = _mapper.Map<PortfolioResponseDto>(createdPortfolioFromDb);

            return CreatedAtAction(nameof(GetPortfolioByIdAsync), new { id = portfolioResponse.Id }, portfolioResponse);
        }


        [HttpGet("{id}")]
        [ActionName(nameof(GetPortfolioByIdAsync))]
        [ProducesResponseType(typeof(PortfolioResponseDto), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get Portfolio by Id", OperationId = "GetPortfolioById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully Fetched the portfolio for the given id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PortfolioResponseDto>> GetPortfolioByIdAsync(Guid id)
        {
            var portfolioFromDb = await _portfolioService.GetPortfoliosByIdAsync(id);
            if (portfolioFromDb is null)
            {
                return NotFound();
            }
            var portfolioResponse = _mapper.Map<PortfolioResponseDto>(portfolioFromDb);

            return Ok(portfolioResponse);
        }

        [HttpGet("GetAllPortfoliosForTenant")]
        [ProducesResponseType(typeof(PortfolioResponseDto), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get all Portfolio for the provided tenant", OperationId = "GetAllPortfoliosForTenant")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully Fetched the portfolios for provided tenant")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PortfolioResponseDto>> GetAllPortfoliosForTenantAsync(Guid tenantId)
        {
            var portfoliosFromDb = await _portfolioService.GetAllPortfoliosByTenantIdAsync(tenantId);
            if (portfoliosFromDb.Count == 0)
            {
                return NotFound("No portfolio record found for provided Tenant Id");
            }
            var portfolioResponse = _mapper.Map<List<PortfolioResponseDto>>(portfoliosFromDb);

            return Ok(portfolioResponse);
        }
               

        [HttpDelete("DeletePortfolioForTenant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Delete Portfolio for provided tenant", OperationId = "DeletePortfolioForTenantAsync")]

        [SwaggerResponse(StatusCodes.Status200OK, "Removed the Portfolio for provided tenant Id")]
        public async Task<IActionResult> DeletePortfolioForTenantAsync(Guid tenantId)
        {
            var portfoliosFromDb = await _portfolioService.GetAllPortfoliosByTenantIdAsync(tenantId);
            if (portfoliosFromDb is null || portfoliosFromDb.Count() <= 0)
            {
                return NotFound($"Portfolio does not exist for the Tenant Id = {tenantId}");
            }

            await _portfolioService.DeletePortfolioByTenantIdAsync(tenantId);
            return NoContent();
        }

    }
}
