using Microsoft.EntityFrameworkCore;
using PortfolioHub.Domain.Models;
using PortfolioHub.Domain.RepositoryContract;

namespace PortfolioHub.Infrastructure.Efcore.RepositoryProvider
{
    public class UserPlantsRepository : Repository<UserPlants>, IUserPlantsRepository
    {
        public UserPlantsRepository(PortfolioHubContext portfolioHubContext) : base(portfolioHubContext) { }

        public async Task<UserPlants> AddUserPlantAsync(UserPlants userPlantsCreationRequest)
        {
            await AddAsync(userPlantsCreationRequest);
            await SaveChangesAsync();
            return userPlantsCreationRequest;
        }

        public async Task DeleteUserPlantsByIdAsync(Guid id)
        {
            var userPlant = await GetUserPlantsByUserIdAsync(id);

            if (userPlant is null)
            {
                throw new Exception($"Plant not exist => {id}.");
            }

            Delete(userPlant);
            await SaveChangesAsync();
        }

        public async Task<UserPlants?> GetUserPlantsByIdAsync(Guid id)
        {
            return await FindByPredicate(x => x.Id == id).Include(x => x.Plants).FirstOrDefaultAsync();
        }

        public async Task<List<UserPlants>> GetUserPlantsByUserIdAsync(Guid userId)
        {
            return await FindByPredicate(x => x.UserId == userId).Include(x => x.Plants).ToListAsync();
        }
    }
}
