using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.EmulationCommendation;
using Misa_TruongWeb03.DL.Repository.Base;

namespace Misa_TruongWeb03.DL.Repository.EmulationCommendationRepository
{
    /// <summary>
    /// Repository Emulation Commendation
    /// Kế thừa các thuộc tính lớp base repo
    /// </summary>
    /// CreatedBy: NQTruong (24/05/2023)
    public interface IEmulationCommendationRepository : IBaseRepository<EmulationCommendation, GetEmulationCommendationDTO, PostEmulationCommendationDTO, UpdateEmulationCommendationDTO>
    {
    }
}
