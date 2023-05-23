using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;

namespace Misa_TruongWeb03.BL.Service.EmulationTitleService
{
    public interface IEmulationTitleService : IBaseService<EmulationTitle, GetEmulationTitle, PostEmulationTitle, UpdateEmulationTitle>
    {
        Task<BaseEntity> Post(PostEmulationTitle model);
        Task<BaseEntity> Put(int id, PostEmulationTitle model);
        Task<BaseEntity> DeleteMultiple(DeleteEmulationTitle model);
    }
}
