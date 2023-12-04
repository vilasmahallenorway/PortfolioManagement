namespace PortfolioHub.Domain.Models
{
    public class Portfolios
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string PortfolioName { get; set; } = string.Empty;
        public virtual Tenants Tenants { get; set; }
        public virtual ICollection<Plants> Plants { get; set; }
    }
}
