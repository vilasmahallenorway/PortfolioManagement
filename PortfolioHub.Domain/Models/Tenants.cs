namespace PortfolioHub.Domain.Models
{
    public class Tenants
    {
        public Guid Id { get; set; }
        public string TenantName { get; set; } = string.Empty;
        public string TenantCountry { get; set; } = string.Empty;
        public virtual ICollection<Portfolios> Portfolios { get; set; }
    }
}
