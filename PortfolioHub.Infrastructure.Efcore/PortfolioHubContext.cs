using Microsoft.EntityFrameworkCore;
using PortfolioHub.Domain.Models;

namespace PortfolioHub.Infrastructure.Efcore
{
    public class PortfolioHubContext : DbContext
    {
        public PortfolioHubContext(DbContextOptions<PortfolioHubContext> options) : base(options) { }
        public virtual DbSet<Tenants> Tenants { get; set; }
        public virtual DbSet<Portfolios> Portfolios { get; set; }
        public virtual DbSet<Plants> Plants { get; set; }
        public virtual DbSet<UserPlants> UserPlants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortfolioHubContext).Assembly);
        }
    }
}