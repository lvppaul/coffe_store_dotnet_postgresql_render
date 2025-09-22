using PRN232.Lab1.CoffeeStore.Services.Exceptions;
using System.Net;
using System.Text.Json;

namespace PRN232.Lab1.CoffeeStore.APIS.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException vex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                var result = new
                {
                    statusCode = 400,
                    message = vex.Message,
                    errors = vex.Errors
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
            catch (KeyNotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    statusCode = 404,
                    message = ex.Message
                }));
            }
            catch (InvalidOperationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    statusCode = 400,
                    message = ex.Message
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    statusCode = 500,
                    message = "Unexpected error occurred",
                    details = ex.Message
                }));
            }
        }
    }
}
