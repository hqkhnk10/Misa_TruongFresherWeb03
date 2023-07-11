using Dapper;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.DL.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.AnswerRepo
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        #region Constructor
        public AnswerRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
        #region MyRegion
        /// <summary>
        /// Thêm câu hỏi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> PostMultiple(Guid questionId, Guid exerciseId, List<Answer> answerList)
        {
            using var connection = this.GetConnection();
            try
            {
                connection.Open();
                var query = "INSERT INTO Answer (AnswerId, AnswerContent, AnswerStatus ,CreatedBy ,ModifiedBy ,QuestionId,ExerciseId) VALUES ";
                for (int index = 0; index < answerList.Count; index++)
                {
                    query += $"(UUID(),'{answerList[index].AnswerContent}',{answerList[index].AnswerStatus},'NQTruong', 'NQTruong', '{questionId}','{exerciseId}')";
                    if(index != answerList.Count - 1)
                    {
                        query += ",";
                    }
                }
                query += ";";
                return await connection.ExecuteAsync(query);

            }
            catch (Exception)
            {
                throw;
            }
            finally { connection.Close(); }
        }
        /// <summary>
        /// Xóa câu trả lời của câu hỏi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> DeleteMultiple(Guid QuestionId)
        {
            using var connection = this.GetConnection();
            try
            {
                connection.Open();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("QuestionId", QuestionId);
                var query = "DELETE FROM Answer a where a.QuestionId = @QuestionId";
                return await connection.ExecuteAsync(query, dynamicParams);
            }
            catch (Exception)
            {
                throw;
            }
            finally { connection.Close(); }
        }
        #endregion
    }
}
