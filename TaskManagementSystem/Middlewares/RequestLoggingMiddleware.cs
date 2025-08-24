namespace TaskManagement.APi.Middleware
{
    public class RequestLoggingMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger<RequestLoggingMiddleware> _logger;

            public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
            {
                _next = next ?? throw new ArgumentNullException(nameof(next));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task InvokeAsync(HttpContext context)
            {
                _logger.LogInformation("Request: {Method} {Path} at {Time}",
                    context.Request.Method,
                    context.Request.Path,
                    DateTime.UtcNow);

                await _next(context);

                _logger.LogInformation("Response: {StatusCode} for {Method} {Path} at {Time}",
                    context.Response.StatusCode,
                    context.Request.Method,
                    context.Request.Path,
                    DateTime.UtcNow);
            }
        }
    
}
