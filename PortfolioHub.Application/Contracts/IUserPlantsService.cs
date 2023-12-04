using PortfolioHub.Api.Contracts.Response;
using PortfolioHub.Domain.Models;

namespace PortfolioHub.Application.Contracts
{
    public interface IUserPlantsService
    {
        Task<UserPlantsResponseDto> AddUserPlantAsync(UserPlants userPlantsCreationRequest);

        Task DeleteUserPlantsByIdAsync(Guid id);

        Task<UserPlantsResponseDto> GetUserPlantsByIdAsync(Guid id);

        Task<UserPlantsResponseDto> GetUserPlantsByUserIdAsync(Guid userId);
               
    }
}
