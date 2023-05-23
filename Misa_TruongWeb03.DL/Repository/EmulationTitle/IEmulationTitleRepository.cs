using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.DL.Repository.Base;

namespace Misa_TruongWeb03.DL.Repository.EmulationTitleRepository
{
    public interface IEmulationTitleRepository : IBaseRepository<EmulationTitle, GetEmulationTitle, PostEmulationTitle, UpdateEmulationTitle>
    {
        Task<BaseEntity> DeleteMultiple(DeleteEmulationTitle model);
    }
}
