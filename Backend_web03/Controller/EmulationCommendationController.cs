using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.EmulationCommendationService;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.Controller.Base;

namespace FresherWeb03.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmulationCommendationController : BaseController<EmulationCommendation, GetEmulationCommendationDTO, PostEmulationCommendationDTO, UpdateEmulationCommendationDTO>
    {
        public EmulationCommendationController(IEmulationCommendationService emulationCommendationService) : base(emulationCommendationService)
        {
        }

    }
}
