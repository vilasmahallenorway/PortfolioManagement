using PortfolioHub.Domain.Models;

namespace PortfolioHub.Api.Contracts.Response
{
    public record UserResponseDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
