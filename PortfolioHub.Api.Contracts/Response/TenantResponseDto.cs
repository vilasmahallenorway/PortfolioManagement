using PortfolioHub.Domain.Models;

namespace PortfolioHub.Api.Contracts.Response
{
    public record TenantResponseDto
    {
        public Guid Id { get; set; }
        public string TenantName { get; set; } = string.Empty;
        public string TenantCountry { get; set; } = string.Empty;

        public List<PortfolioResponseDto>? Portfolios { get; set; }
    }
}
