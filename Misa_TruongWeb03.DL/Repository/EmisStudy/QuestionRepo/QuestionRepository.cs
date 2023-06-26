using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.QuestionRepo
{
    public class QuestionRepository : BaseRepository<Question, QuestionGetDTO, QuestionPostDTO, QuestionPutDTO>, IQuestionRepository
    {
        #region Constructor
        public QuestionRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
        #region Method
        /// <summary>
        /// Thêm câu hỏi
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ExerciseId"></param>
        /// <returns></returns>
        public async Task<BaseEntity> Post(QuestionPostDTO model, int? ExerciseId)
        {
            using var connection = this.GetConnection();
            try
            {
                connection.Open();
                // Generate the SQL query to check for duplicates
                var store = "proc_question_insert";
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                // Execute the query with the list of values as a parameter
                var result = await connection.QueryFirstOrDefaultAsync<int?>(store, new { jsonData = jsonString, exerciseId = ExerciseId }, commandType: CommandType.StoredProcedure);
                // If the count is greater than 0, duplicates exist
                return new BaseEntity
                {
                    ErrorCode = StatusCodes.Status200OK,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    DevMsg = ex.Message,
                    UserMsg = VN.Error500
                };
                return exception;
            }
            finally { connection.Close(); }
        }
        /// <summary>
        /// Sửa câu hỏi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseEntity> Put(QuestionPutDTO model)
        {
            using var connection = this.GetConnection();
            try
            {
                connection.Open();
                // Generate the SQL query to check for duplicates
                var store = "proc_exercise_update";
                string jsonString = JsonSerializer.Serialize(model);
                // Execute the query with the list of values as a parameter
                var result = await connection.QueryAsync<int?>(store, new { Id = model.QuestionId, jsonData = jsonString }, commandType: CommandType.StoredProcedure);
                // If the count is greater than 0, duplicates exist
                return new BaseEntity
                {
                    ErrorCode = StatusCodes.Status200OK,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                var exception = new BaseEntity
                {
                    ErrorCode = StatusCodes.Status500InternalServerError,
                    Data = null,
                    DevMsg = ex.Message,
                    UserMsg = VN.Error500
                };
                return exception;
            }
            finally { connection.Close(); }
        } 
        #endregion

    }
}
