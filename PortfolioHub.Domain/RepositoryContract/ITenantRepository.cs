using PortfolioHub.Domain.Models;

namespace PortfolioHub.Domain.RepositoryContract
{
    public interface ITenantRepository
    {
        Task<Tenants> AddTenantAsync(Tenants tenantsCreationRequest);
        Task<Tenants> UpdateTenantAsync(Tenants tenantUpdationRequest);
        Task  DeleteTenantAsync(Guid tenantId);
        Task<Tenants?> GetTenantByIdAsync(Guid tenantId);
        Task<List<Tenants>> GetAllTenantsAsync();
    }
}
