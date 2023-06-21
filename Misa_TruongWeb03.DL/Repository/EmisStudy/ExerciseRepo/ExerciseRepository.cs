﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
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
        public override async Task<BaseEntity> GetById(int Id)
        {
            using var connection = this.GetConnection();
            try
            {
                connection.Open();
                // Generate the SQL query to check for duplicates
                var store = "proc_exercise_getdetail";

                // Execute the query with the list of values as a parameter
                var multipleResult = await connection.QueryMultipleAsync(store, new { Id }, commandType: CommandType.StoredProcedure);
                // If the count is greater than 0, duplicates exist
                var exerciseData = await multipleResult.ReadAsync<DetailExerciseModel>();
                var questionData = await multipleResult.ReadAsync<QuestionDetailModel>();
                var answerData = await multipleResult.ReadAsync<AnswerModel>();
                answerData = answerData.ToList();
                questionData = questionData.Select(q =>
                {
                    q.Answers.AddRange(answerData);
                    return q;
                });
                questionData = questionData.ToList();
                exerciseData = exerciseData.Select(exercise =>
                {
                    exercise.Questions.AddRange(questionData);
                    return exercise;
                }
                    );


                return new BaseEntity
                {
                    ErrorCode = StatusCodes.Status200OK,
                    Data = exerciseData
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

        public async Task<BaseEntity> Post(string jsonModel)
        {
            using var connection = this.GetConnection();
            try
            {
                connection.Open();
                // Generate the SQL query to check for duplicates
                var store = "proc_exercise_insert";

                // Execute the query with the list of values as a parameter
                var result = await connection.QueryAsync<int?>(store, new { json_data = jsonModel }, commandType: CommandType.StoredProcedure);
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
    }
}
