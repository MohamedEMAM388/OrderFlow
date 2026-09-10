using Domain.Exceptions;
using FluentValidation;

namespace API.Exceptions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var (statusCode, message) = MapException(ex);

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                // Unexpected errors: log full details server-side, never leak them to the client.
                _logger.LogError(ex, "Unhandled exception while processing {Path}", httpContext.Request.Path);
            }

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(new { message });
        }
    }

    private static (int StatusCode, object Message) MapException(Exception ex) => ex switch
    {
        NotFoundException => (StatusCodes.Status404NotFound, ex.Message),
        DomainException => (StatusCodes.Status400BadRequest, ex.Message),
        ValidationException validationEx => (
            StatusCodes.Status400BadRequest,
            validationEx.Errors.Select(e => e.ErrorMessage)),
        _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
    };
}