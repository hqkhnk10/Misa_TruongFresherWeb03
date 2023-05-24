using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;

namespace Misa_TruongWeb03.BL.Service.EmulationTitleService
{
    /// <summary>
    /// Service Emulation Title
    /// Kế thừa các thuộc tính lớp base service
    /// </summary>
    /// CreatedBy: QTNgo (24/05/2023)
    public interface IEmulationTitleService : IBaseService<EmulationTitle, GetEmulationTitle, PostEmulationTitle, UpdateEmulationTitle>
    {
        #region Method
        Task<BaseEntity> Post(PostEmulationTitle model);
        Task<BaseEntity> Put(int id, PostEmulationTitle model);
        Task<BaseEntity> DeleteMultiple(DeleteEmulationTitle model); 
        #endregion
    }
}
