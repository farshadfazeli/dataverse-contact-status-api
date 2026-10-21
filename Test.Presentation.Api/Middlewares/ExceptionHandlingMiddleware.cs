using System.Text.Json;
using FluentValidation;

namespace Test.Presentation.Api.Middlewares;

public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An unhandled exception occurred");

            var statusCode = exception is ValidationException
                ? StatusCodes.Status400BadRequest
                : StatusCodes.Status500InternalServerError;

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                status = statusCode,
                title = exception.GetType().Name,
                detail = statusCode == StatusCodes.Status400BadRequest
                    ? exception.Message
                    : "An unexpected error occurred.",
            }));
        }
    }
}
