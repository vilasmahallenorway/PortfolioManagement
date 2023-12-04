using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using PortfolioHub.Domain.Exceptions;

namespace PortfolioHub.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private ILogger _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Process exception and call next middleware
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            try
            {
                _logger = logger;

                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, env);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment env)
        {
            _logger.LogError(ex, ex.Message);

            int status = StatusCodes.Status500InternalServerError;
            string message = string.Empty;
            string additionalInfo = string.Empty;
            var errorDetail = string.Empty;
            var errorCode = -1;
            var exceptionType = ex.GetType();
            bool isHandled = false;

            var errorData = new List<ErrorData>();

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = ErrorMessages.UnauthorizedException;
                status = StatusCodes.Status401Unauthorized;
            }
            else if (exceptionType == typeof(AggregateException))
            {
                foreach (var exception in ((AggregateException)(ex)).InnerExceptions)
                {
                    message = exception.Message;
                    status = StatusCodes.Status500InternalServerError;
                    _logger?.LogError(ex, ex.Message);
                }
            }
            else if (exceptionType == typeof(ArgumentNullException))
            {
                message = ErrorMessages.ArgumentNullException;
                status = StatusCodes.Status400BadRequest;
            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                message = ErrorMessages.KeyNotFoundExceptionDefault;
                status = StatusCodes.Status404NotFound;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = ErrorMessages.NotImplementedException;
                status = StatusCodes.Status404NotFound;
            }
            else if (exceptionType == typeof(ArgumentOutOfRangeException))
            {
                message = ErrorMessages.ArgumentOutOfRangeException;
                status = StatusCodes.Status400BadRequest;
            }
            else if (exceptionType == typeof(ArgumentException))
            {
                message = ErrorMessages.ArgumentException;
                status = StatusCodes.Status400BadRequest;
            }
            else if (exceptionType == typeof(TimeoutException))
            {
                message = ErrorMessages.TimeOutException;
                status = StatusCodes.Status408RequestTimeout;
            }
            else if (exceptionType == typeof(InvalidOperationException))
            {
                message = ErrorMessages.InvalidOperationException;
                status = StatusCodes.Status400BadRequest;
            }
            else if (exceptionType == typeof(InvalidDataException))
            {
                message = ex.Message;
                status = StatusCodes.Status400BadRequest;
            }
            else
            {
                message = ErrorMessages.UnhandledException + ex.Message;
                status = StatusCodes.Status500InternalServerError;
                _logger?.LogError(ex, ex.Message);
            }

            if (env.IsDevelopment())
            {
                message = message + "\n" + ex.Message;
                errorDetail = ex.StackTrace;
            }

            if (!isHandled)
            {
                var errordata = new ErrorData
                {
                    Message = message,
                    DateTime = DateTime.UtcNow,
                    RequestUri = context.Request.GetDisplayUrl(),
                    AdditionalInfo = additionalInfo,
                    ErrorDetail = errorDetail,
                    ErrorCode = errorCode,
                    HttpStatus = status,
                };
                errorData.Add(errordata);
            }

            string result = string.Empty;

            if (errorData.Count > 0)
            {
                result = errorData.Count > 1 ? JsonConvert.SerializeObject(errorData) : JsonConvert.SerializeObject(errorData[0]);
            }
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;
            return context.Response.WriteAsync(result);
        }
    }
}
