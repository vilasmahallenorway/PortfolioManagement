using PortfolioHub.Domain.Models;

namespace PortfolioHub.Domain.RepositoryContract
{
    public interface IPortfolioRepository
    {
        Task<Portfolios> AddPortfolioAsync(Portfolios portfoliosCreationRequest);
        Task DeletePortfolioByTenantIdAsync(Guid tenantId);
        Task<Portfolios?> GetPortfolioByIdAsync(Guid portfolioId);
        Task<List<Portfolios>> GetAllPortfoliosByTenantIdAsync(Guid tenantId);
    }
}
