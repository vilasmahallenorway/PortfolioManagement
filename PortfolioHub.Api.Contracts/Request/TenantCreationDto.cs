namespace PortfolioHub.Api.Contracts.Request
{
    public record TenentCreationDto
    {
        public string TenantName { get; set; } = string.Empty;
        public string TenantCountry { get; set; } = string.Empty;
    }
}
