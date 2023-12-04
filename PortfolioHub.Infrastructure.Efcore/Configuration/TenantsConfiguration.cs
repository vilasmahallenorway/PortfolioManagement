using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioHub.Domain.Models;

namespace PortfolioHub.Infrastructure.Efcore.Configuration
{
    public class TenantsConfiguration : IEntityTypeConfiguration<Tenants>
    {
        public void Configure(EntityTypeBuilder<Tenants> builder)
        {
            builder.ToTable("Tenants");

            builder.HasKey(x => x.Id);
            builder.Property(p => p.TenantName).HasMaxLength(100).IsRequired(true);
            builder.Property(p => p.TenantCountry).HasMaxLength(60).IsRequired(true);
        }
    }
}
