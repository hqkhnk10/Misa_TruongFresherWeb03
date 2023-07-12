using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Entity.Base
{
    /// <summary>
    /// Server Error
    /// </summary>
    public class ExceptionError : ServiceResponse
    {
        public ExceptionError(Exception ex)
        {
            Data = null;
            ErrorCode = StatusCodes.Status500InternalServerError;
            DevMsg = ex.Message;
            UserMsg = VN.Error500;
        }
    }
    /// <summary>
    /// Lớp lỗi từ database ( database không thay đổi dữ liệu)
    /// Created By: NQTruong (25/05/2023)
    /// </summary>
    public class DatabaseError : ServiceResponse
    {
        public DatabaseError()
        {
            Data = null;
            ErrorCode = StatusCodes.Status500InternalServerError;
            DevMsg = VN.NoAffectedRows;
            UserMsg = VN.Error500;
        }
    }
    /// <summary>
    /// Database trả ra kết quả null
    /// Created By: NQTruong (25/05/2023)
    /// </summary>
    public class DatabaseReturnNullError : ServiceResponse
    {
        public DatabaseReturnNullError()
        {
            Data = null;
            ErrorCode = StatusCodes.Status500InternalServerError;
            DevMsg = VN.NoAffectedRows;
            UserMsg = VN.Error500;
        }
    }
    /// <summary>
    /// Database trả ra kết quả 0
    /// Created By: NQTruong (25/05/2023)
    /// </summary>
    public class DatabaseReturn0Error : ServiceResponse
    {
        public DatabaseReturn0Error()
        {
            Data = 0;
            ErrorCode = StatusCodes.Status500InternalServerError;
            DevMsg = VN.NoAffectedRows;
            UserMsg = VN.Error500;
        }
    }
    /// <summary>
    /// Không tìm thấy bản ghi
    /// Created By: NQTruong (25/05/2023)
    /// </summary>
    public class NotFoundError : ServiceResponse
    {
        public NotFoundError()
        {
            ErrorCode = StatusCodes.Status404NotFound;
            DevMsg = VN.Error404;
            UserMsg = VN.Error404;
        }
    }
    /// <summary>
    /// Lỗi trùng dữ liệu
    /// Created By: NQTruong (25/05/2023)
    /// </summary>
    public class DuplicateError : ServiceResponse
    {
        public DuplicateError()
        {
            ErrorCode = StatusCodes.Status409Conflict;
            DevMsg = VN.DuplicateError;
            UserMsg = VN.DuplicateError;
        }
    }

    /// <summary>
    /// Lỗi trùng dữ liệu
    /// Created By: NQTruong (25/05/2023)
    /// </summary>
    public class BadRequestError : ServiceResponse
    {
        public BadRequestError(string message)
        {
            ErrorCode = StatusCodes.Status400BadRequest;
            DevMsg = message;
            UserMsg = message;
        }
    }

}
