using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.EmisStudy.QuestionService;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.Controller.Base;

namespace Misa_TruongWeb03.Emis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : BaseController<Question,QuestionGetDTO, QuestionPostDTO, QuestionPutDTO>
    {
        #region Constructor
        public QuestionController(IQuestionService questionService) : base(questionService)
        {
        }
        #endregion
    }
}
