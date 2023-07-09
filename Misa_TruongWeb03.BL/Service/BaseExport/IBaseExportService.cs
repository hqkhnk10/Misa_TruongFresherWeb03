using Misa_TruongWeb03.Common.Entity.FileEntity;

namespace Misa_TruongWeb03.BL.Service.BaseExport
{
    public interface IBaseExportService<T>
    {
        Task<byte[]> ExportFile(string fileName, ExportModel model);
        byte[]? GetSampleFile(string cacheKey);
    }
}