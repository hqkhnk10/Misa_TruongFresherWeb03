﻿using AutoMapper;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.DL.Repository.EmisStudy.ExerciseRepo;
using System.Text.Json;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.ExerciseService
{
    public class ExerciseService : BaseService<Exercise, ExerciseGetDTO, ExercisePostDTO, ExercisePutDTO>, IExerciseService
    {
        #region Property
        private readonly IExerciseRepository _exerciseRepository; 
        #endregion
        #region Constructor
        public ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper) : base(exerciseRepository, mapper)
        {
            _exerciseRepository = exerciseRepository;
        }
        #endregion
        #region Method
        /// <summary>
        /// Thêm bài tập
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// CreatedBy: NQTruong (20/06/2023)
        public async Task<BaseEntity> Post(ExercisePostDTO model)
        {
            string jsonString = JsonSerializer.Serialize(model);
            var result = await _exerciseRepository.Post(jsonString);
            return result;
        }
        #endregion
    }
}
