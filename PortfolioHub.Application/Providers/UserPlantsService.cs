using PortfolioHub.Api.Contracts.Response;
using PortfolioHub.Application.Contracts;
using PortfolioHub.Domain.Models;
using PortfolioHub.Domain.RepositoryContract;

namespace PortfolioHub.Application.Providers
{
    public class UserPlantsService : IUserPlantsService
    {
        private readonly IUserPlantsRepository _userPlantsRepository;
        public UserPlantsService(IUserPlantsRepository userPlantRepository)
        {
            this._userPlantsRepository = userPlantRepository ?? throw new ArgumentNullException(nameof(userPlantRepository));
        }

        public async Task<UserPlantsResponseDto> AddUserPlantAsync(UserPlants userPlantsCreationRequest)
        {
            UserPlantsResponseDto? objUserPlantsResponseDto = null;
            var userPlantFromDb = await _userPlantsRepository.AddUserPlantAsync(userPlantsCreationRequest);
            if (userPlantFromDb != null)
            {
                objUserPlantsResponseDto = await GetUserPlantsByUserIdAsync(userPlantFromDb.UserId);
            }
            return objUserPlantsResponseDto;
        }

        public async Task DeleteUserPlantsByIdAsync(Guid id)
        {
            await _userPlantsRepository.DeleteUserPlantsByIdAsync(id);
        }

        public async Task<UserPlantsResponseDto> GetUserPlantsByIdAsync(Guid id)
        {
            UserPlantsResponseDto? objUserPlantsResponseDto = null;
            var userPlantFromDb = await _userPlantsRepository.GetUserPlantsByIdAsync(id);

            if (userPlantFromDb != null)
            {
                PlantResponseDto plantResp = new()
                {
                    Id = userPlantFromDb.Plants.Id,
                    PlantName = userPlantFromDb.Plants.PlantName,
                    PortfolioId = userPlantFromDb.Plants.PortfolioId
                };

                List<PlantResponseDto> lstPlantResponseDto = new()
                {
                    plantResp
                };

                objUserPlantsResponseDto = new()
                {
                    UserId = userPlantFromDb.UserId,
                    Id = userPlantFromDb.Id,
                    Plants = lstPlantResponseDto
                };
            }
            return objUserPlantsResponseDto;
        }

        public async Task<UserPlantsResponseDto> GetUserPlantsByUserIdAsync(Guid userId)
        {
            UserPlantsResponseDto? objUserPlantsResponseDto = null;

            var userPlantFromDb = await _userPlantsRepository.GetUserPlantsByUserIdAsync(userId);
            List<PlantResponseDto> lstPlantResponseDto = new();
            if (userPlantFromDb != null)
            {
                foreach (var plantItem in userPlantFromDb.Select(x => x.Plants))
                {
                    PlantResponseDto plantResp = new()
                    {
                        Id = plantItem.Id,
                        PlantName = plantItem.PlantName,
                        PortfolioId = plantItem.PortfolioId
                    };
                    lstPlantResponseDto.Add(plantResp);
                }

                objUserPlantsResponseDto = new()
                {
                    UserId = userPlantFromDb.First().UserId,
                    Id = userPlantFromDb.First().Id,
                    Plants = lstPlantResponseDto
                };

            }
            return objUserPlantsResponseDto;
        }
    }
}
