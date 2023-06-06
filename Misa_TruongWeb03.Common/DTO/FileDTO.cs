using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Misa_TruongWeb03.Common.DTO
{
    public class FileDTO
    {
    }
    public class ValidateFileDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        [DefaultValue(0)]
        public int SheetIndex { get; set; }
        [Required]
        [DefaultValue(1)]
        public int Header { get; set; }
        [Required]
        [DefaultValue("emulationtitle")]
        public string Key { get; set; }

    }
    public class FileValidateModel
    {
        public object? ValidData { get; set; }
        public object? InValidData { get; set; }
        public string FileName { get; set; }
        public int Count { get; set; }
    }
}
