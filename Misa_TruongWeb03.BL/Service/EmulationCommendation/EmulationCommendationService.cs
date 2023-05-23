using AutoMapper;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.DL.Repository.EmulationCommendationRepository;

namespace Misa_TruongWeb03.BL.Service.EmulationCommendationService
{
    public class EmulationCommendationService : BaseService<EmulationCommendation, GetEmulationCommendationDTO, PostEmulationCommendationDTO, UpdateEmulationCommendationDTO>, IEmulationCommendationService
    {
        public EmulationCommendationService(IEmulationCommendationRepository baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
        }
    }
}
