namespace PortfolioHub.Domain.Exceptions
{
    public class ErrorData
    {
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public string RequestUri { get; set; }
        public string AdditionalInfo { get; set; }
        public string ErrorDetail { get; set; }
        public int ErrorCode { get; set; }
        public int HttpStatus { get; set; }
    }
}
