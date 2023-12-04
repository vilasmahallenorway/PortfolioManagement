using PortfolioHub.Application.Contracts;
using PortfolioHub.Domain.Models;
using PortfolioHub.Domain.RepositoryContract;

namespace PortfolioHub.Application.Providers
{
    public class TenantsService : ITenantsService
    {
        private readonly ITenantRepository _tenantRepository;
        public TenantsService(ITenantRepository tenantRepository)
        {
            this._tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository));
        }
        public async Task<Tenants> AddTenantAsync(Tenants tenants)
        {
            return await _tenantRepository.AddTenantAsync(tenants);
        }

        public async Task DeleteTenantAsync(Guid tenantId)
        {
            await _tenantRepository.DeleteTenantAsync(tenantId);
        }

        public async Task<List<Tenants>> GetAllTenantsAsync()
        {
            return await _tenantRepository.GetAllTenantsAsync();
        }

        public async Task<Tenants?> GetTenantByIdAsync(Guid tenantId)
        {
            return await _tenantRepository.GetTenantByIdAsync(tenantId);
        }

        public async Task<Tenants> UpdateTenantAsync(Tenants tenants)
        {
            return await _tenantRepository.UpdateTenantAsync(tenants);
        }
    }
}
