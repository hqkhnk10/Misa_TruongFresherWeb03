using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.EmulationCommendationService;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.Controller.Base;

namespace FresherWeb03.Controller
{
    /// <summary>
    /// Cấp khen thưởng Controller
    /// kế thừa các CRUD từ Base Controller
    /// </summary>
    /// CreatedBy: QTNgo (24/05/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmulationCommendationController : BaseController<EmulationCommendation, GetEmulationCommendationDTO, PostEmulationCommendationDTO, UpdateEmulationCommendationDTO>
    {
        #region Constructor
        public EmulationCommendationController(IEmulationCommendationService emulationCommendationService) : base(emulationCommendationService)
        {
        } 
        #endregion

    }
}
