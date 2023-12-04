using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortfolioHub.Domain.RepositoryContract;
using PortfolioHub.Infrastructure.Efcore.RepositoryProvider;

namespace PortfolioHub.Infrastructure.Efcore.Extensions
{
    public static class EfCoreServiceCollectionExtensions
    {
        public static void AddEfRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var userConnectionString = configuration.GetConnectionString("PortfolioConnection");

            services.AddDbContext<PortfolioHubContext>(o => o.UseSqlServer(userConnectionString, (opts) => opts.EnableRetryOnFailure()));

            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<IPlantRepository, PlantRepository>();
            services.AddScoped<IUsersRepository, UserRepository>();
            services.AddScoped<IUserPlantsRepository, UserPlantsRepository>();
        }
    }
}
