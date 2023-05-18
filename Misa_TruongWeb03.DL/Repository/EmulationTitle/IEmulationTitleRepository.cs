using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;

namespace Misa_TruongWeb03.DL.Repository.EmulationTitle
{
    public interface IEmulationTitleRepository
    {
        Task<BaseEntity> Get(GetEmulationTitle model);
        Task<BaseEntity> GetDetail(int id);
        Task<BaseEntity> Post(PostEmulationTitle model);
        Task<BaseEntity> Put(UpdateEmulationTitle model);
        Task<BaseEntity> Delete(int id);
        Task<BaseEntity> DeleteMultiple(DeleteEmulationTitle model);
    }
}
