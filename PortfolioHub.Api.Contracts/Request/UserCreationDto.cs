namespace PortfolioHub.Api.Contracts.Request
{
    public record UserCreationDto
    {
        public string UserName { get; set; } = string.Empty;
    }
}
