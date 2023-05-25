using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.DL.Repository.Base;

namespace Misa_TruongWeb03.DL.Repository.EmulationTitleRepository
{
    /// <summary>
    /// Interface danh hiệu thi đua
    /// Kế thừa lớp base repo
    /// </summary>
    /// CreatedBy: QTNgo (24/05/2023)
    public interface IEmulationTitleRepository : IBaseRepository<EmulationTitle, GetEmulationTitle, PostEmulationTitle, UpdateEmulationTitle>
    {
        #region Method
        Task<BaseEntity> DeleteMultiple(DeleteEmulationTitle model);
        Task<BaseEntity> UpdateStatus(UpdateEmulationTitleStatusDto model);
        Task<BaseEntity> UpdateMultipleStatus(UpdateMultipleEmulationTitleStatusDto model);
        #endregion
    }
}
