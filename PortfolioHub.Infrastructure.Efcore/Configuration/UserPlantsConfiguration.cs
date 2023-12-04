using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioHub.Domain.Models;

namespace PortfolioHub.Infrastructure.Efcore.Configuration
{
    public class UserPlantsConfiguration : IEntityTypeConfiguration<UserPlants>
    {
        public void Configure(EntityTypeBuilder<UserPlants> builder)
        {
            builder.ToTable("UserPlants");

            builder.HasKey(p => p.Id);

            builder.HasOne(x=> x.Users)
                 .WithMany(y => y.UserPlantsDetails)
                   .HasForeignKey(x => x.UserId)
                    .HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.Plants)
                .WithMany(y => y.UserPlantsDetails)
                  .HasForeignKey(x => x.PlantId)
                   .HasPrincipalKey(x => x.Id);
        }
    }
}
