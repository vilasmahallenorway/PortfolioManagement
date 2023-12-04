using PortfolioHub.Application.Contracts;
using PortfolioHub.Domain.Models;
using PortfolioHub.Domain.RepositoryContract;

namespace PortfolioHub.Application.Providers
{
    public class PortfoliosService : IPortfoliosService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfoliosService(IPortfolioRepository portfolioRepository)
        {
            this._portfolioRepository = portfolioRepository ?? throw new ArgumentNullException(nameof(portfolioRepository));
        }

        public async Task<Portfolios> AddPortfolioAsync(Portfolios portfolioCreationRequest)
        {
            return await _portfolioRepository.AddPortfolioAsync(portfolioCreationRequest);
        }

        public async Task DeletePortfolioByTenantIdAsync(Guid tenantId)
        {
           await _portfolioRepository.DeletePortfolioByTenantIdAsync(tenantId);
        }
       
        public async Task<List<Portfolios>> GetAllPortfoliosByTenantIdAsync(Guid tenantId)
        {
            return await _portfolioRepository.GetAllPortfoliosByTenantIdAsync(tenantId);
        }

        public async Task<Portfolios?> GetPortfoliosByIdAsync(Guid portfolioId)
        {
            return await _portfolioRepository.GetPortfolioByIdAsync(portfolioId);
        }
    }
}
