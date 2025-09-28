using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Text.Json;
using System.Diagnostics;
using PerlaMetroApiMain.Exceptions;


namespace PerlaMetroApiMain.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly Dictionary<Type, (string ErrorMessage, int StatusCode, string Code)> exceptionMapping =
        new()
        {
            { typeof(UnauthorizedAccessException), ("Unauthorized access", 401, "unauthorized") },
            { typeof(OperationCanceledException), ("Operation canceled", 499, "operation_canceled") },
            { typeof(InternalErrorException), ("Internal server error", 500, "internal_error") }
        };

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger instance.</param>
    public ExceptionHandlerMiddleware(RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware to handle exceptions and generate appropriate HTTP responses.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <returns>A task that represents the completion of request processing.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DownstreamServiceException downstream)
        {
            await WriteDownstreamResponse(context, downstream);
        }
        catch (Exception ex)
        {
            if (exceptionMapping.TryGetValue(ex.GetType(), out var mapping))
            {
                await GenerateHttpResponse(ex, context, mapping.ErrorMessage, mapping.StatusCode, mapping.Code);
            }
            else
            {
                await GenerateHttpResponse(ex, context, "Internal Server Error", 500, "internal_error");
            }
        }
    }

    /// <summary>
    /// Generates an HTTP response based on the provided exception and context.
    /// </summary>
    /// <param name="ex">The exception that occurred.</param>
    /// <param name="context">The current HTTP context.</param>
    /// <param name="errorTitle">The title of the error.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="code">The error code.</param>
    /// <returns>A task that represents the completion of the response generation.</returns>
    private async Task GenerateHttpResponse(
        Exception ex,
        HttpContext context,
        string errorTitle,
        int statusCode,
        string code
    )
    {
        if (statusCode >= 500)
            _logger.LogError(ex, ex.Message);
        else
            _logger.LogWarning(ex, ex.Message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new ProblemDetails
        {
            Status = statusCode,
            Title = errorTitle,
            Detail = ex.Message,
            Instance = context.Request.Path, // Internal API URL that caused the error
            Type = $"https://httpstatuses.io/{statusCode}"
        };
        response.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;
        response.Extensions["code"] = code;

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
    }

    private async Task WriteDownstreamResponse(HttpContext context, DownstreamServiceException exception)
    {
        _logger.LogWarning(exception, "Downstream service responded with status code {StatusCode}", exception.StatusCode);

        context.Response.ContentType = exception.ContentType;
        context.Response.StatusCode = exception.StatusCode;

        await context.Response.WriteAsync(exception.ResponseBody ?? string.Empty);
    }
}

/// <summary>
/// Extension methods for registering the ExceptionHandlerMiddleware.
/// </summary>
public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseProblemDetailsExceptionHandler(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
