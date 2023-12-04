using Microsoft.Extensions.DependencyInjection;
using PortfolioHub.Application.Contracts;
using PortfolioHub.Application.Providers;

namespace PortfolioHub.Application.Extensions
{
    public static class ServiceContainerExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITenantsService, TenantsService>();
            services.AddScoped<IPortfoliosService, PortfoliosService>();
            services.AddScoped<IPlantsService, PlantsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IUserPlantsService, UserPlantsService>();
        }
    }
}
