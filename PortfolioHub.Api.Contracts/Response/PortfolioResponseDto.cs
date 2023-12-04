using PortfolioHub.Domain.Models;

namespace PortfolioHub.Api.Contracts.Response
{
    public record PortfolioResponseDto
    {
        public Guid Id { get; set; }
        public string PortfolioName { get; set; } = string.Empty;
        public Guid TenantId { get; set; }

        public List<PlantResponseDto>? Plants { get; set; }
    }
}
