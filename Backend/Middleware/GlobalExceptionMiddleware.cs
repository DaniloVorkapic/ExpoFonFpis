using System.Text.Json;
using Backend.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Middleware
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                ProblemDetails details = new()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "Server Error",
                    Title = "Server Error",
                    Detail = "An Internal Server Error has occured"
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(details));
            }
        }
    }
}
