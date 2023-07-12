using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Entity.Base
{

    public class BaseException : Exception
    {
        public int ErrorCode { get; set; }
        public object ErrorData { get; set; }
        public string ErrorMsg { get; set; }
        public string UserMsg { get; set; }
    }
    public class InternalException : BaseException
    {
        public InternalException(Exception e)
        {
            ErrorCode = StatusCodes.Status500InternalServerError;
            ErrorMsg = e.Message;
        }
    }
    public class NotFoundException : BaseException
    {
        public NotFoundException()
        {
            ErrorCode = StatusCodes.Status404NotFound;
            ErrorMsg = VN.Error404;
        }
    }
    public class BadRequestException : BaseException
    {
        public BadRequestException()
        {
            ErrorCode = StatusCodes.Status400BadRequest;
            ErrorMsg = VN.Error;
        }
    }
    public class DuplicateException : BaseException
    {
        public DuplicateException()
        {
            ErrorCode = StatusCodes.Status409Conflict;
            ErrorMsg = VN.DuplicateError;
        }
    }
    public class DatabaseExeception : BaseException
    {
        public DatabaseExeception()
        {
            ErrorCode = StatusCodes.Status500InternalServerError;
            ErrorMsg = VN.DatabaseError;
        }
    }
}
