using Misa_TruongWeb03.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Entity.Base
{
    public class GetParams : FilterModel
    {
        public Dictionary<string, object> Filters { get; set; } = new Dictionary<string, object>();
    }
    /// <summary>
    /// Model phân trang
    /// </summary>
    /// CreatedBy: NQTruong (24/05/2023)
    public class FilterModel
    {
        /// <summary>
        /// Tổng số bản ghi/ trang
        /// </summary>
        /// CreatedBy: NQTruong (24/05/2023)
        public int PageSize { get; set; } = 100;
        /// <summary>
        /// Trang số
        /// </summary>
        /// CreatedBy: NQTruong (24/05/2023)
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        /// CreatedBy: NQTruong (24/05/2023)
        public string? Keyword { get; set; } = null;
    }
}
