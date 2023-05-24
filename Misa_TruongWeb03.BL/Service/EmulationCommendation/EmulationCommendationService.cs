using AutoMapper;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.DL.Repository.EmulationCommendationRepository;

namespace Misa_TruongWeb03.BL.Service.EmulationCommendationService
{
    /// <summary>
    /// Tầng Service của cấp phong trào
    /// Kế thừa CRUD từ base
    /// </summary>
    /// CreatedBy: QTNgo (24/05/2023)
    public class EmulationCommendationService : BaseService<EmulationCommendation, GetEmulationCommendationDTO, PostEmulationCommendationDTO, UpdateEmulationCommendationDTO>, IEmulationCommendationService
    {
        #region Constructor
        public EmulationCommendationService(IEmulationCommendationRepository baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
        } 
        #endregion
    }
}
