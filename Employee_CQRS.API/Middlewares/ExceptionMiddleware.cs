using Employee_CQRS.API.Models;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Employee_CQRS.API.Middlewares;

/// <summary>
/// Global exception handling middleware.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
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
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation error");

            await HandleExceptionAsync(
                context,
                HttpStatusCode.BadRequest,
                "Validation failed",
                ex.Errors.Select(e => e.ErrorMessage).ToList()
            );
        }
        catch (KeyNotFoundException ex)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.NotFound,
                ex.Message
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            await HandleExceptionAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Something went wrong. Please try again later."
            );
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string message,
        List<string>? errors = null)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = ApiResponse<string>.FailureResponse(
            message,
            context.Response.StatusCode,
            errors
        );

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response)
        );
    }
}
