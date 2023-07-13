using Dapper;
using Dapper.Transaction;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.Common.Helper;
using Misa_TruongWeb03.DL.Entity.Base;
using Misa_TruongWeb03.DL.Repository.Base;
using Misa_TruongWeb03.DL.Repository.UnitOfWorkk;
using System.Data;
using System.Data.Common;
using static Dapper.SqlMapper;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.ExerciseRepo
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        #region Constructor
        public ExerciseRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
        {
        }
        #endregion

    }
}
