using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.FileEntity;

namespace Misa_TruongWeb03.BL.Service.Import
{
    public interface IBaseImportService<T>
    {
        dynamic? GetSampleFile(string cacheKey);
        Task<FileValidateModel> Validate(IFormFile file, int sheetIndex, int header);
        Task<bool> IsDuplicateRecord(object cellValue);
        Task<byte[]> ExportFile(string fileName, ExportModel model);
        Task<dynamic> Get(dynamic parameters);
    }
}