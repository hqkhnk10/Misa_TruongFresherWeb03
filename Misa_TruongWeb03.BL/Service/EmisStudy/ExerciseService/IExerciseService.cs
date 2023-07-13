using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using System.Data.Common;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.ExerciseService
{
    public interface IExerciseService: IBaseService<Exercise,ExerciseDTO, ExerciseGetDTO,ExercisePostDTO,ExercisePutDTO>
    {
        Task<Guid> AddOrUpdate(ExercisePostDTO model, DbTransaction transaction);
    }
}