using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioHub.Domain.Models;

namespace PortfolioHub.Infrastructure.Efcore.Configuration
{
    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.UserName).HasMaxLength(100).IsRequired(true);
        }
    }
}
