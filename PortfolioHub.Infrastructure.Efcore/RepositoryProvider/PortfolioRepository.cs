using Microsoft.EntityFrameworkCore;
using PortfolioHub.Domain.Models;
using PortfolioHub.Domain.RepositoryContract;

namespace PortfolioHub.Infrastructure.Efcore.RepositoryProvider
{
    public class PortfolioRepository : Repository<Portfolios>, IPortfolioRepository
    {
        public PortfolioRepository(PortfolioHubContext portfolioHubContext) : base(portfolioHubContext)
        {

        }

        public async Task<Portfolios> AddPortfolioAsync(Portfolios portfolioCreationRequest)
        {
            await AddAsync(portfolioCreationRequest);
            await SaveChangesAsync();
            return portfolioCreationRequest;
        }

        public async Task DeletePortfolioByTenantIdAsync(Guid tenantId)
        {
            var portfolios = await GetAllPortfoliosByTenantIdAsync(tenantId);

            if (portfolios is null || portfolios.Count() <= 0)
            {
                throw new Exception($"Portfolio not exist for Tenant Id => {tenantId}.");
            }

            foreach (var portfolio in portfolios)
            {
                Delete(portfolio);

            }
            await SaveChangesAsync();
        }

        public async Task<List<Portfolios>> GetAllPortfoliosByTenantIdAsync(Guid tenantId)
        {
            return await FindByPredicate(x => x.TenantId == tenantId).Include(x => x.Plants)
               .ToListAsync();
        }

        public async Task<Portfolios?> GetPortfolioByIdAsync(Guid portfolioId)
        {
            return await FindByPredicate(x => x.Id == portfolioId).Include(x => x.Plants)
                  .FirstOrDefaultAsync();
        }
    }
}
