using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.EmulationCommendation;

namespace Misa_TruongWeb03.BL.Service.EmulationCommendationService
{
    /// <summary>
    /// Service Emulation Commendation
    /// Kế thừa các thuộc tính lớp base service
    /// </summary>
    /// CreatedBy: NQTruong (24/05/2023)
    public interface IEmulationCommendationService : IBaseService<EmulationCommendation, GetEmulationCommendationDTO, PostEmulationCommendationDTO, UpdateEmulationCommendationDTO>
    {
    }
}
