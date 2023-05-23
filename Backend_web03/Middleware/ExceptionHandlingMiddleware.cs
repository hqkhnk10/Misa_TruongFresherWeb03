using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Middleware;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred.");

            // Generate a unique trace ID
            string traceId = Guid.NewGuid().ToString();

            // Handle the exception and create a custom response object
            var responseObject = new
            {
                ErrorCode = 500,
                DevMsg = ex.Message,
                UserMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                StatusCode = StatusCodes.Status500InternalServerError,
                TraceId = traceId,
                MoreInfo = ex.Source,
            };

            // Set the response content type to JSON
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // Serialize the response object to JSON
            string responseJson = System.Text.Json.JsonSerializer.Serialize(responseObject);

            // Write the JSON response to the response body
            await context.Response.WriteAsync(responseJson);
        }
    }
}

