﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Misa_TruongWeb03.Common.Resource;
using System;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Middleware;
/// <summary>
/// Middleware xử lí exception
/// </summary>
/// CreatedBy: NQTruong (24/05/2023)
public class ExceptionHandlingMiddleware
{
    #region Property
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    #endregion

    #region Constructor
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    #endregion

    #region Event
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
                UserMsg = VN.SystemError,
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
    #endregion
}

