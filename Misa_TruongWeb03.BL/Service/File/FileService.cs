using Aspose.Cells;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.FileEntity;
using Misa_TruongWeb03.DL.Repository.FileRepository;
using System.Drawing;

namespace Misa_TruongWeb03.BL.Service.FileServices
{
    public class FileService : IFileService
    {
        #region Property
        private readonly IWebHostEnvironment _env; 
        #endregion
        #region Constuctor
        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }
        #endregion
        #region Method
        /// <summary>
        /// Tải file lên server
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// Created By: NQTruong (01/06/2023)
        public async Task<FileModel> Upload(IFormFile file)
        {
            try
            {
                var uploadsFolder = Path.Combine(_env.ContentRootPath, "FileStorage");

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using var stream = File.Create(filePath);

                await file.CopyToAsync(stream);

                var fileObject = new FileModel
                {
                    FileStoreName = uniqueFileName,
                    FilePath = filePath,
                    FileSize = file.Length,
                    FileName = file.FileName
                };
                return fileObject;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// Tải file theo tên
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// Created By: NQTruong (01/06/2023)
        public dynamic? Download(string filename)
        {
            var filePath = Path.Combine(_env.ContentRootPath, "FileStorage", filename);
            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }
            return System.IO.File.ReadAllBytes(filePath);
        } 
        #endregion

    }
}
