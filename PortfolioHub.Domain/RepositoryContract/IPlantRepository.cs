using PortfolioHub.Domain.Models;

namespace PortfolioHub.Domain.RepositoryContract
{
    public interface IPlantRepository
    {
        Task<Plants> AddPlantAsync(Plants plantsCreationRequest);
        Task DeletePlantByPortfolioIdAsync(Guid portfolioId);
        Task<Plants?> GetPlantByIdAsync(Guid plantId);
        Task<List<Plants>> GetAllPlantsByPortfolioIdAsync(Guid portfolioId);
    }
}
