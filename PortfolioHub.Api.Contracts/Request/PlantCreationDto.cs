namespace PortfolioHub.Api.Contracts.Request
{
    public class PlantCreationDto
    {
        public Guid PortfolioId { get; set; }
        public string PlantName { get; set; } = string.Empty;
    }
}
