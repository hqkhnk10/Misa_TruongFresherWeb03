namespace Misa_TruongWeb03.Common.Entity
{
    /// <summary>
    /// Return class of every service, repo
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object? Data { get; set; }
        /// <summary>
        /// Trạng thái trả về
        /// </summary>
        public int ErrorCode { get; set; } = 200;
        /// <summary>
        /// Thông báo cho dev
        /// </summary>
        public string DevMsg { get; set; } = string.Empty;
        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        public string UserMsg { get; set; } = "Thành công";
        /// <summary>
        /// Thông tin cụ thể về lỗi
        /// </summary>
        public string MoreInfo { get; set; } = string.Empty;
        /// <summary>
        /// Nã để tra cứu thông tin
        /// </summary>
        public System.Diagnostics.ActivityTraceId TraceId { get; set; }
        /// <summary>
        /// Bản ghi bị thay đổi
        /// </summary>
        public int Rows { get; set; } = 0;
        /// <summary>
        /// Phân trang
        /// </summary>
        public Pagination? Pagination { get; set; }
    }
    /// <summary>
    /// Phân trang
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Tổng bản ghi
        /// </summary>
        public int? Count { set; get; }
        /// <summary>
        /// Trang số
        /// </summary>
        public int? PageIndex { set; get; }
        /// <summary>
        /// Số bản ghi tối đa ở 1 trang
        /// </summary>
        public int? PageSize { set; get; }
        /// <summary>
        /// Tổng số trang
        /// </summary>
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
