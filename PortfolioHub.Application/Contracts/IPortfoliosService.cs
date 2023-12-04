using PortfolioHub.Domain.Models;

namespace PortfolioHub.Application.Contracts
{
    public interface IPortfoliosService
    {
        Task<Portfolios> AddPortfolioAsync(Portfolios portfolioCreationRequest);

        Task DeletePortfolioByTenantIdAsync(Guid tenantId);

        Task<Portfolios?> GetPortfoliosByIdAsync(Guid portfolioId);

        Task<List<Portfolios>> GetAllPortfoliosByTenantIdAsync(Guid tenantId);
    }
}
