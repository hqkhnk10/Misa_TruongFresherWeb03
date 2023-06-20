using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.BL.Service.EmisStudy.AnswerService;
using Misa_TruongWeb03.BL.Service.EmulationCommendationService;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using Misa_TruongWeb03.Controller.Base;

namespace Misa_TruongWeb03.Emis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : BaseController<Answer,AnswerGetDTO,AnswerPostDTO,AnswerPutDTO>
    {
        #region Constructor
        public AnswerController(IAnswerService answerService) : base(answerService)
        {
        }
        #endregion
    }
}
