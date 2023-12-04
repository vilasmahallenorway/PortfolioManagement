namespace PortfolioHub.Api.Contracts.Request
{
    public class UserPlantsCreationDto
    {
        public Guid UserId { get; set; }

        public Guid PlantId { get; set; }
    }
}
