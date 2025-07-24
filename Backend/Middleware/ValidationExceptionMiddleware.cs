namespace Backend.Middleware
{
    using FluentValidation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    namespace Evidencija.Middleware
    {
        public class ValidationExceptionMiddleware : IMiddleware
        {
            private readonly ILogger<ValidationExceptionMiddleware> _logger;

            public ValidationExceptionMiddleware(ILogger<ValidationExceptionMiddleware> logger)
            {
                _logger = logger;
            }

            public async Task InvokeAsync(HttpContext context, RequestDelegate next)
            {
                try
                {
                    await next(context);
                }
                catch (ValidationException ex)
                {
                    _logger.LogWarning(ex, "Validation failed");

                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";

                    var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

                    var problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Type = "Validation Error",
                        Title = "Validation Error",
                        Detail = "One or more validation errors occurred.",
                        Extensions = { { "errors", errors } }
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
                }
            }
        }
    }
}
