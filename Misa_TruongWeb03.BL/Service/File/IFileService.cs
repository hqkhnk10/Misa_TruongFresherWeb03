using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.FileEntity;

namespace Misa_TruongWeb03.BL.Service.FileServices
{
    /// <summary>
    /// Tầng service xử lí file
    /// Created By: NQTruong (01/06/2023)
    /// </summary>
    public interface IFileService
    {
        #region Method
        Task<FileModel> Upload(IFormFile file);
        dynamic? Download(string fileName); 
        #endregion

    }
}