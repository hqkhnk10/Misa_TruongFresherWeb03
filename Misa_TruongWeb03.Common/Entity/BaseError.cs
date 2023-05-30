using Misa_TruongWeb03.Common.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Entity
{
    /// <summary>
    /// Lớp lỗi từ database ( database không thay đổi dữ liệu)
    /// Created By: NQTruong (25/05/2023)
    /// </summary>
    public class DatabaseError : BaseEntity
    {
        public DatabaseError()
        {
            Data = null;
            ErrorCode = 500;
            DevMsg = VN.NoAffectedRows;
            UserMsg = VN.Error500;
        }
    }
    /// <summary>
    /// Database trả ra kết quả null
    /// Created By: NQTruong (25/05/2023)
    /// </summary>
    public class DatabaseReturnNullError : BaseEntity
    {
        public DatabaseReturnNullError()
        {
            Data = null;
            ErrorCode = 500;
            DevMsg = VN.NoAffectedRows;
            UserMsg = VN.Error500;
        }
    }
    /// <summary>
    /// Database trả ra kết quả 0
    /// Created By: NQTruong (25/05/2023)
    /// </summary>
    public class DatabaseReturn0Error : BaseEntity
    {
        public DatabaseReturn0Error()
        {
            Data = 0;
            ErrorCode = 500;
            DevMsg = VN.NoAffectedRows;
            UserMsg = VN.Error500;
        }
    }
    /// <summary>
    /// Không tìm thấy bản ghi
    /// Created By: NQTruong (25/05/2023)
    /// </summary>
    public class NotFoundError : BaseEntity
    {
        public NotFoundError()
        {
            ErrorCode = 404;
            DevMsg = VN.Error404;
            UserMsg = VN.Error404;
        }
    }
    /// <summary>
    /// Lỗi trùng dữ liệu
    /// Created By: NQTruong (25/05/2023)
    /// </summary>
    public class DuplicateError : BaseEntity
    {
        public DuplicateError()
        {
            ErrorCode = 302;
            DevMsg = VN.DuplicateError;
            UserMsg = VN.DuplicateError;
        }
    }
}
