using System.Net;
using System.Text.Json;

namespace TaskManagement.APi.Middleware
{
    public class ExceptionMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger<ExceptionMiddleware> _logger;

            public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
            {
                _next = next ?? throw new ArgumentNullException(nameof(next));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)GetStatusCode(ex);

                    var errorResponse = new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message,
                        Details = ex.StackTrace
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                }
            }

            private HttpStatusCode GetStatusCode(Exception ex)
            {
                return ex switch
                {
                    KeyNotFoundException => HttpStatusCode.NotFound,
                    UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                    ArgumentException => HttpStatusCode.BadRequest,
                    _ => HttpStatusCode.InternalServerError
                };
            }
        }
}
