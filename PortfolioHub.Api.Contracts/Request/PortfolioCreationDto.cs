namespace PortfolioHub.Api.Contracts.Request
{
    public record PortfolioCreationDto
    {
        public Guid TenantId { get; set; }
        public string PortfolioName { get; set; } = string.Empty;
    }
}
