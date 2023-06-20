﻿using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.DL.Repository.Base;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.ExerciseRepo
{
    public interface IExerciseRepository : IBaseRepository<Exercise, ExerciseGetDTO, ExercisePostDTO, ExercisePutDTO>
    {
        Task<BaseEntity> Post(string jsonModel);
    }
}