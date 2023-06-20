using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.ExerciseService
{
    public interface IExerciseService: IBaseService<Exercise, ExerciseGetDTO,ExercisePostDTO,ExercisePutDTO>
    {
    }
}