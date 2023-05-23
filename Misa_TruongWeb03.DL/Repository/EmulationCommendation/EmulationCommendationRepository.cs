using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.DL.Repository.Base;

namespace Misa_TruongWeb03.DL.Repository.EmulationCommendationRepository
{
    public class EmulationCommendationRepository : BaseRepository<EmulationCommendation, GetEmulationCommendationDTO, PostEmulationCommendationDTO, UpdateEmulationCommendationDTO>, IEmulationCommendationRepository
    {
        #region Constructor
        public EmulationCommendationRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
    }
}
