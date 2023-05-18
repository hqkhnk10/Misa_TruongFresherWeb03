namespace Misa_TruongWeb03.Common.Entity
{
    public class BaseEntity
    {
        public object? Data { get; set; }
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = string.Empty;
        public int Count { get; set; } = 0;
        public Pagination? Pagination { get; set; }
    }
    public class Pagination
    {
        public int Count { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public int TotalPage
        {
            get
            {
                return (int)Math.Ceiling((float)Count / (float)PageSize);
            }
        }

    }
}
