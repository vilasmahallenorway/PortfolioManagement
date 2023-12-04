namespace PortfolioHub.Domain.Models
{
    public class UserPlants
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PlantId { get; set; }
        public virtual Users? Users { get; set; }
        public virtual Plants? Plants { get; set; }
    }
}
