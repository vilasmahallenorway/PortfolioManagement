using PortfolioHub.Domain.Models;

namespace PortfolioHub.Application.Contracts
{
    public interface ITenantsService
    {
        Task<Tenants> AddTenantAsync(Tenants tenants);

        Task<Tenants> UpdateTenantAsync(Tenants tenants);

        Task DeleteTenantAsync(Guid tenantId);

        Task<Tenants?> GetTenantByIdAsync(Guid tenantId);

        Task<List<Tenants>> GetAllTenantsAsync();
    }
}
