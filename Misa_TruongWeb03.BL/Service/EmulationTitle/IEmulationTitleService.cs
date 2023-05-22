using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;

namespace Misa_TruongWeb03.BL.Service.EmulationTitle
{
    public interface IEmulationTitleService
    {
        Task<BaseEntity> Get(GetEmulationTitle model);
        Task<BaseEntity> GetDetail(int id);
        Task<BaseEntity> Post(PostEmulationTitle model);
        Task<BaseEntity> Put(int id, PostEmulationTitle model);
        Task<BaseEntity> Delete(int id);
        Task<BaseEntity> DeleteMultiple(DeleteEmulationTitle model);
    }
}
