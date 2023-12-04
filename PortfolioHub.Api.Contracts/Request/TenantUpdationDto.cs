namespace PortfolioHub.Api.Contracts.Request
{
    public class TenantUpdationDto
    {
        public Guid Id { get; set; }
        public string TenantName { get; set; } = string.Empty;
        public string TenantCountry { get; set; } = string.Empty;
    }
}
