using Microsoft.EntityFrameworkCore;
using PortfolioHub.Domain.Models;
using PortfolioHub.Domain.RepositoryContract;

namespace PortfolioHub.Infrastructure.Efcore.RepositoryProvider
{
    public class PlantRepository : Repository<Plants>, IPlantRepository
    {
        public PlantRepository(PortfolioHubContext portfolioHubContext) : base(portfolioHubContext)
        {

        }

        public async Task<Plants> AddPlantAsync(Plants plantsCreationRequest)
        {
            await AddAsync(plantsCreationRequest);
            await SaveChangesAsync();
            return plantsCreationRequest;
        }

        public async Task DeletePlantByPortfolioIdAsync(Guid portfolioId)
        {
            var plants = await GetAllPlantsByPortfolioIdAsync(portfolioId);

            if (plants is null || plants.Count <= 0)
            {
                throw new Exception($"Plant not exist for {portfolioId}.");
            }

            foreach (var plant in plants)
            {
                Delete(plant);
                await SaveChangesAsync();
            }
        }

        public async Task<List<Plants>> GetAllPlantsByPortfolioIdAsync(Guid portfolioId)
        {
            return await FindByPredicate(x => x.PortfolioId == portfolioId)
               .ToListAsync();
        }

        public async Task<Plants?> GetPlantByIdAsync(Guid plantId)
        {
            return await FindByPredicate(x => x.Id == plantId)
                  .FirstOrDefaultAsync();
        }
    }
}
