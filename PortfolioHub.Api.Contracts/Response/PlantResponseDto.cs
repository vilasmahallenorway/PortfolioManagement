using PortfolioHub.Domain.Models;

namespace PortfolioHub.Api.Contracts.Response
{
    public record PlantResponseDto
    {
        public Guid Id { get; set; }
        public Guid PortfolioId { get; set; }
        public string PlantName { get; set; } = string.Empty;
    }
}
