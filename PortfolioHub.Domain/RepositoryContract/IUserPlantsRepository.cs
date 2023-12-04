using PortfolioHub.Domain.Models;

namespace PortfolioHub.Domain.RepositoryContract
{
    public interface IUserPlantsRepository
    {
        Task<UserPlants> AddUserPlantAsync(UserPlants plantsCreationRequest);
        Task DeleteUserPlantsByIdAsync(Guid id);
        Task<UserPlants?> GetUserPlantsByIdAsync(Guid userPlantId);
        Task<List<UserPlants>> GetUserPlantsByUserIdAsync(Guid userId);
    }
}
