using PortfolioHub.Application.Contracts;
using PortfolioHub.Domain.Models;
using PortfolioHub.Domain.RepositoryContract;

namespace PortfolioHub.Application.Providers
{
    public class PlantsService : IPlantsService
    {
        private readonly IPlantRepository _plantsRepository;
        public PlantsService(IPlantRepository plantRepository)
        {
            this._plantsRepository = plantRepository ?? throw new ArgumentNullException(nameof(plantRepository));
        }

        public async Task<Plants> AddPlantsAsync(Plants plantCreationRequest)
        {
            return await _plantsRepository.AddPlantAsync(plantCreationRequest);
        }
        public async Task DeletePlantsByPortfolioIdAsync(Guid portfolioId)
        {
            await _plantsRepository.DeletePlantByPortfolioIdAsync(portfolioId);
        }
        public async Task<List<Plants>> GetAllPlantsByPortfolioIdAsync(Guid portfolioId)
        {
            return await _plantsRepository.GetAllPlantsByPortfolioIdAsync(portfolioId);
        }
        public async Task<Plants?> GetPlantByIdAsync(Guid plantId)
        {
            return await _plantsRepository.GetPlantByIdAsync(plantId);
        }
    }
}
