using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.EmisStudy.GradeService;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Grade;
using Misa_TruongWeb03.Controller.Base;

namespace Misa_TruongWeb03.Emis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : BaseController<Grade, Grade, GradeGetDTO, GradePostDTO, GradePutDTO>
    {
        #region Constructor
        public GradeController(IGradeService gradeService) : base(gradeService)
        {
        }
        #endregion
    }
}
