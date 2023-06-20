using AutoMapper;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.DL.Repository.EmisStudy.ExerciseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.ExerciseService
{
    public class ExerciseService : BaseService<Exercise, ExerciseGetDTO, ExercisePostDTO, ExercisePutDTO>, IExerciseService
    {
        #region Constructor
        public ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper) : base(exerciseRepository, mapper)
        {
        }
        #endregion
    }
}
