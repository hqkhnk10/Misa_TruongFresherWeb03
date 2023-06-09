﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Entity.Base;
using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Files.Middleware;
/// <summary>
/// Middleware xử lí exception
/// </summary>
/// CreatedBy: NQTruong (24/05/2023)
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Khởi tạo đối tượng middleware ExceptionMiddleware
    /// lưu trữ tham chiếu đến middleware tiếp theo trong pipeline để sử dụng
    /// sau đó để chuyển yêu cầu đến middleware tiếp theo.
    /// </summary>
    /// <param name="next"> tham số kiểu RequestDelegate</param>
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Khi có yêu cầu HTTP đi qua middleware,
    /// sẽ thực hiện gọi phương thức tiếp theo (_next) trong pipeline 
    /// và bắt exception nếu có.
    /// </summary>
    /// <param name="context">Các thông tin về yêu cầu web hiện tại</param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    /// <summary>
    /// Phân loại và xử lý chính xác các exception bắt được
    /// </summary>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        var userMsg = "";
        switch (exception)
        {
            case BadRequestException e:
                // bad request error
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                userMsg = VN.Error;
                break;
            case NotFoundException e:
                // not found error
                response.StatusCode = (int)HttpStatusCode.NotFound;
                userMsg = VN.Error404;
                break;
            case DuplicateException e:
                // not found error
                response.StatusCode = (int)HttpStatusCode.Conflict;
                userMsg = VN.DuplicateError;
                break;
            case BaseException e:
                // custom exception
                response.StatusCode = e.ErrorCode;
                userMsg = e.ErrorMsg;
                break;
            default:
                // unhandled error
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                userMsg = VN.Error500;
                break;
        }
        var result = JsonSerializer.Serialize(new { Data = exception.Data,DevMsg = exception?.Message, UserMsg = userMsg });
        await response.WriteAsync(result);
    }
}

