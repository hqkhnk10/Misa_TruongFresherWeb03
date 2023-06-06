using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.FileEntity;
using System.Dynamic;

namespace Misa_TruongWeb03.BL.Service.BaseImport
{
    /// <summary>
    /// Base xử lí xuất/nhập khẩu
    /// </summary>
    /// <typeparam name="T">Entity Model</typeparam>
    public interface IBaseImportService<T>
    {
        #region Method
        dynamic? GetSampleFile(string cacheKey);
        Task<FileValidateModel> Validate(IFormFile file, int sheetIndex, int header);
        Task<bool> IsDuplicateRecord(object cellValue);
        Task<byte[]> ExportFile(string fileName, ExportModel model);
        Task<dynamic> Get(dynamic parameters); 
        #endregion
    }
}