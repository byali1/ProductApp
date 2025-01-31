using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace ProductApp.Infrastructure.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // unique Request ID 
            var requestId = context.TraceIdentifier;
            var requestPath = context.Request.Path;
            var queryParams = context.Request.QueryString.Value;

            _logger.LogInformation($"[Request ID: {requestId}] - Path: {requestPath} | Query: {queryParams}");

            context.Response.Headers.Add("X-Request-ID", requestId);

            await _next(context); // Pipeline'ın devam etmesi
        }
    }
}
