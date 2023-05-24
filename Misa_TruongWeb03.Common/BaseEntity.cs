namespace Misa_TruongWeb03.Common.Entity
{
    /// <summary>
    /// Return class of every service, repo
    /// </summary>
    /// CreatedBy: QTNgo (24/05/2023)
    public class BaseEntity
    {
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public object? Data { get; set; }
        /// <summary>
        /// Trạng thái trả về
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public int ErrorCode { get; set; } = 200;
        /// <summary>
        /// Thông báo cho dev
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public string DevMsg { get; set; } = string.Empty;
        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public string UserMsg { get; set; } = "Thành công";
        /// <summary>
        /// Thông tin cụ thể về lỗi
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public string MoreInfo { get; set; } = string.Empty;
        /// <summary>
        /// Nã để tra cứu thông tin
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public System.Diagnostics.ActivityTraceId TraceId { get; set; }
        /// <summary>
        /// Bản ghi bị thay đổi
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public int Rows { get; set; } = 0;
        /// <summary>
        /// Phân trang
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public Pagination? Pagination { get; set; }
    }
    /// <summary>
    /// Phân trang
    /// </summary>
    /// CreatedBy: QTNgo (24/05/2023)
    public class Pagination
    {
        /// <summary>
        /// Tổng bản ghi
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public int? Count { set; get; }
        /// <summary>
        /// Trang số
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public int? PageIndex { set; get; }
        /// <summary>
        /// Số bản ghi tối đa ở 1 trang
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public int? PageSize { set; get; }
        /// <summary>
        /// Tổng số trang
        /// </summary>
        /// CreatedBy: QTNgo (24/05/2023)
        public int? TotalPage
        {
            get
            {
                if (Count != null && PageSize != null) {
                    return (int)Math.Ceiling((float)Count / (float)PageSize);
                }
                else return 0;
            }
        }

    }
}
