using Microsoft.AspNetCore.Mvc;

namespace PortfolioHub.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    public class BaseController :  ControllerBase
    {
      
    }
}
