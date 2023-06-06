using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;

namespace Misa_TruongWeb03.BL.Service.FileServices
{
    public interface IFileService
    {
        Task<FileModel> Upload(IFormFile file);
        dynamic? Download(string fileName);

    }
}