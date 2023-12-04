using PortfolioHub.Domain.Models;

namespace PortfolioHub.Application.Contracts
{
    public interface IPlantsService
    {
        Task<List<Plants>> GetAllPlantsByPortfolioIdAsync(Guid plantId);

        Task<Plants?> GetPlantByIdAsync(Guid  plantId);

        Task<Plants> AddPlantsAsync(Plants plantCreationRequest);

        Task DeletePlantsByPortfolioIdAsync(Guid plantId);
    }
}
