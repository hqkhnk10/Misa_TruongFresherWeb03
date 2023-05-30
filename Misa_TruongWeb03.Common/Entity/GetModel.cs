using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Entity
{
    /// <summary>
    /// Model phân trang
    /// </summary>
    /// CreatedBy: NQTruong (24/05/2023)
    public class GetModel
    {
        /// <summary>
        /// Tổng số bản ghi/ trang
        /// </summary>
        /// CreatedBy: NQTruong (24/05/2023)
        public int pageSize { get; set; } = 10;
        /// <summary>
        /// Trang số
        /// </summary>
        /// CreatedBy: NQTruong (24/05/2023)
        public int pageIndex { get; set; } = 1;
        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        /// CreatedBy: NQTruong (24/05/2023)
        public string? keyword { get; set; } = null;
    }
}
