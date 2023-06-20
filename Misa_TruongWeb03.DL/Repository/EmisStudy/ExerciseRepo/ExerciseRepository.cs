using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.DL.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.ExerciseRepo
{
    public class ExerciseRepository : BaseRepository<Exercise, ExerciseGetDTO, ExercisePostDTO, ExercisePutDTO>, IExerciseRepository
    {
        #region Constructor
        public ExerciseRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
    }
}
