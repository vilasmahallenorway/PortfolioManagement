using PortfolioHub.Domain.Models;

namespace PortfolioHub.Api.Contracts.Response
{
    public record UserPlantsResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<PlantResponseDto>? Plants { get; set; }
    }
}
