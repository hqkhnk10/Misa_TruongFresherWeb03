using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.Common.Entity.FileEntity;

namespace Misa_TruongWeb03.DL.Repository.FileRepository
{
    /// <summary>
    /// Tầng repo kết nối với db lấy mapping config
    /// </summary>
    public interface IFileRepository
    {
        public Task<List<ExcelConfigEntity>> MappingConfig(string key);
    }
}