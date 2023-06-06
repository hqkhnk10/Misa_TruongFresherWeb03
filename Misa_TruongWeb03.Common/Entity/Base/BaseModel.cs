using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Entity.Base
{
    /// <summary>
    /// Dữ liệu trả về của mọi bảng
    /// </summary>
    /// Created By: NQTruong (24/05/2023)
    public abstract class BaseModel
    {
        /// <summary>
        /// Tổng bản ghi
        /// </summary>
        /// Created By: NQTruong (22/05/2023)
        public int Count { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// Created By: NQTruong (22/05/2023)
        public DateTime? CreatedAt { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// Created By: NQTruong (22/05/2023)
        public DateTime? ModifiedAt { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        /// Created By: NQTruong (22/05/2023)
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        /// Created By: NQTruong (22/05/2023)
        public string? ModifiedBy { get; set; }
    }
}
