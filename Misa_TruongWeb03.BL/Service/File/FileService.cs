using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Misa_TruongWeb03.BL.Service.FileService
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> Upload(IFormFile Image)
        {

            var uploadsFolder = Path.Combine(_env.ContentRootPath, "FileStorage");

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var stream = File.Create(filePath);

            await Image.CopyToAsync(stream);

            return uniqueFileName;

        }
    }
}
