using Dapper;
using Dapper.Transaction;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.Common.Helper;
using Misa_TruongWeb03.DL.Repository.Base;
using System.Data;
using System.Data.Common;
using static Dapper.SqlMapper;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.ExerciseRepo
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        #region Constructor
        public ExerciseRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
        #region Method
        /// <summary>
        /// Lấy bài tập theo Id
        /// </summary>
        /// <param name="Id"></param>
        /// CreatedBy: NQTruong (20/06/2023)
        /// <returns></returns>
        public async Task<IEnumerable<DetailExerciseModel>> GetDetail(Guid Id)
        {
            using var connection = this.GetConnection();

            connection.Open();
            var store = "proc_exercise_getdetail";

            var multipleResult = await connection.QueryMultipleAsync(store, new { Id }, commandType: CommandType.StoredProcedure);


            var exerciseData = await multipleResult.ReadAsync<DetailExerciseModel>();
            var questionData = await multipleResult.ReadAsync<QuestionDetailModel>();
            var answerData = await multipleResult.ReadAsync<AnswerModel>();


            answerData = answerData.ToList();
            questionData = questionData.Select(q =>
            {
                q.Answers.AddRange(answerData.Where(a => a.QuestionId == q.QuestionId));
                return q;
            });
            questionData = questionData.ToList();
            exerciseData = exerciseData.Select(exercise =>
            {
                exercise.Questions.AddRange(questionData);
                return exercise;
            }
                );

            connection.Close();
            return exerciseData;
        }
        /// <summary>
        /// Thêm bài tập
        /// </summary>
        /// <param name="jsonModel"></param>
        /// CreatedBy: NQTruong (20/06/2023)
        /// <returns></returns>
        public async Task<Guid> Post(Exercise model, DbTransaction transaction)
        {
            var storedProcedureName = GenerateProcName.Generate<Exercise>("Post");
            Guid newGuid = Guid.NewGuid();
            var parameters = DynamicParametersAdd.CreateParameterDynamic(model);
            parameters.Add("Id", newGuid);
            var result = await transaction.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            return newGuid;
        }
        public async Task<Guid> Put(Guid exerciseId, Exercise model, DbTransaction transaction)
        {
            var storedProcedureName = GenerateProcName.Generate<Exercise>("Put");
            Guid newGuid = Guid.NewGuid();
            var parameters = DynamicParametersAdd.CreateParameterDynamic(model);
            parameters.Add("Id", newGuid);
            parameters.Add("ExerciseId", exerciseId);
            var result = await transaction.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            return newGuid;
        }
        #endregion
    }
}
