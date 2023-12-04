using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioHub.Domain.Models;

namespace PortfolioHub.Infrastructure.Efcore.Configuration
{
    public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolios>
    {
        public void Configure(EntityTypeBuilder<Portfolios> builder)
        {
            builder.ToTable("Portfolios");

            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.PortfolioName).HasMaxLength(100).IsRequired(true);

            builder.HasOne(x=> x.Tenants)
                 .WithMany(y => y.Portfolios)
                   .HasForeignKey(x => x.TenantId)
                    .HasPrincipalKey(x => x.Id);
        }
    }
}
