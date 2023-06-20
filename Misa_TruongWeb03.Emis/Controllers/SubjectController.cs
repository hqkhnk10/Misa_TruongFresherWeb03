using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.EmisStudy.SubjectService;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Supject;
using Misa_TruongWeb03.Controller.Base;

namespace Misa_TruongWeb03.Emis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : BaseController<Subject, SubjectGetDTO, SubjectPostDTO, SubjectPutDTO>
    {
        #region Constructor
        public SubjectController(ISubjectService subjectService) : base(subjectService)
        {
        }
        #endregion
    }
}
