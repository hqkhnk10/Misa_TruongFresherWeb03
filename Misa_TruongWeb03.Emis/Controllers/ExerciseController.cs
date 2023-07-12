using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.EmisStudy.ExerciseService;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.Controller.Base;

namespace Misa_TruongWeb03.Emis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : BaseController<Exercise, ExerciseDTO, ExerciseGetDTO, ExercisePostDTO, ExercisePutDTO>
    {
        #region Constructor
        public ExerciseController(IExerciseService exerciseService) : base(exerciseService)
        {
        }
        #endregion
    }
}
