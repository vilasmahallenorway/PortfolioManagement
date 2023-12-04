namespace PortfolioHub.Domain.Models
{
    public class Plants
    {
        public Guid Id { get; set; }
        public Guid PortfolioId { get; set; }
        public string PlantName { get; set; } = string.Empty;
        public virtual Portfolios Portfolios { get; set; }
        public virtual ICollection<UserPlants> UserPlantsDetails { get; set; }
    }
}
