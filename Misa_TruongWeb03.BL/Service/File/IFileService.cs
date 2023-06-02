using Microsoft.AspNetCore.Http;

namespace Misa_TruongWeb03.BL.Service.FileService
{
    public interface IFileService
    {
        Task<string> Upload(IFormFile Image);
    }
}