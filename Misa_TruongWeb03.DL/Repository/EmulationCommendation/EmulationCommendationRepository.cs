using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.EmulationCommendation;
using Misa_TruongWeb03.DL.Repository.Base;

namespace Misa_TruongWeb03.DL.Repository.EmulationCommendationRepository
{
    /// <summary>
    /// Repo của cấp phong trào
    /// Kế thừa CRUD từ base
    /// </summary>
    /// CreatedBy: NQTruong (24/05/2023)
    public class EmulationCommendationRepository : BaseRepository<EmulationCommendation, GetEmulationCommendationDTO, PostEmulationCommendationDTO, UpdateEmulationCommendationDTO>, IEmulationCommendationRepository
    {
        #region Constructor
        public EmulationCommendationRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
    }
}
