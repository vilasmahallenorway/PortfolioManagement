using Microsoft.EntityFrameworkCore;
using PortfolioHub.Domain.Models;
using PortfolioHub.Domain.RepositoryContract;

namespace PortfolioHub.Infrastructure.Efcore.RepositoryProvider
{
    public class TenantRepository : Repository<Tenants>, ITenantRepository
    {
        public TenantRepository(PortfolioHubContext portfolioHubContext) : base(portfolioHubContext)
        {

        }
        public async Task<Tenants> AddTenantAsync(Tenants tenantsCreationRequest)
        {
            await AddAsync(tenantsCreationRequest);
            await SaveChangesAsync();
            return tenantsCreationRequest;
        }

        public async Task DeleteTenantAsync(Guid tenantId)
        {
            var tenant = await GetTenantByIdAsync(tenantId) ?? throw new Exception($"tenant not exist.");
            Delete(tenant);
            await SaveChangesAsync();
        }

        public async Task<List<Tenants>> GetAllTenantsAsync()
        {
           return  await GetAll().Include(x=>x.Portfolios).ToListAsync();
        }

        public async Task<Tenants?> GetTenantByIdAsync(Guid tenantId)
        {
            return await FindByPredicate(x => x.Id == tenantId)
                     .Include(x => x.Portfolios)
                     .FirstOrDefaultAsync();
        }

        public async Task<Tenants> UpdateTenantAsync(Tenants tenant)
        {
            await UpdateAsync(tenant);
            await SaveChangesAsync();
            return tenant;
        }
    }
}
