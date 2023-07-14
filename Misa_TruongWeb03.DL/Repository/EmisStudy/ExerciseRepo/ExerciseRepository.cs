using Dapper;
using Dapper.Transaction;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.Common.Helper;
using Misa_TruongWeb03.DL.Entity.Base;
using Misa_TruongWeb03.DL.Model;
using Misa_TruongWeb03.DL.Repository.Base;
using Misa_TruongWeb03.DL.Repository.UnitOfWorkk;
using System.Data;
using System.Data.Common;
using static Dapper.SqlMapper;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.ExerciseRepo
{
    public class ExerciseRepository : BaseRepository<Exercise, ExerciseGetModel>, IExerciseRepository
    {
        #region Constructor
        public ExerciseRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
        {
        }
        #endregion
        #region Method
        protected override string BuildQuery(string select, string where, string limit)
        {
            var customSelect = "SELECT e.*,g.gradeName,s.subjectName,Count(q.QuestionId) as Question FROM exercise e" +
                " Left join Grade g on g.GradeId = e.GradeId Left join Subject s on s.SubjectId = e.ExerciseId Left join Question q on q.ExerciseId = e.ExerciseId";
            var groupby = "Group by e.exerciseId ";
            return customSelect + where + groupby + limit;
        }
        #endregion
    }
}
