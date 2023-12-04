using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioHub.Domain.Models;

namespace PortfolioHub.Infrastructure.Efcore.Configuration
{
    public class PlantsConfiguration : IEntityTypeConfiguration<Plants>
    {
        public void Configure(EntityTypeBuilder<Plants> builder)
        {
            builder.ToTable("Plants");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.PlantName).HasMaxLength(100).IsRequired(true);

            builder.HasOne(x => x.Portfolios)
                 .WithMany(y => y.Plants)
                   .HasForeignKey(x => x.PortfolioId)
                    .HasPrincipalKey(x => x.Id);
        }
    }
}
