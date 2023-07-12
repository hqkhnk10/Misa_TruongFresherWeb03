﻿using Dapper;
using Dapper.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.Common.Helper;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.QuestionRepo
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
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
        public async Task<Guid> Post(Question model, Guid? ExerciseId, DbTransaction transaction)
        {
                var newGuid = Guid.NewGuid();
                var parameters = DynamicParametersAdd.CreateParameterDynamic(model);
                parameters.Add("ExerciseId", ExerciseId);
                parameters.Add("QuestionId", newGuid);
                var store = "proc_question_insert";
                var result = await transaction.ExecuteAsync(store, parameters, commandType: CommandType.StoredProcedure);
                return newGuid;
        }
        /// <summary>
        /// Sửa câu hỏi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> Put(Guid id, Guid ExcerciseId, Question model, DbTransaction transaction)
        {
          
                var parameters = DynamicParametersAdd.CreateParameterDynamic(model);
                parameters.AddDynamicParams(new { id });
                var store = "proc_question_update";
                var result = await transaction.ExecuteAsync(store, parameters, commandType: CommandType.StoredProcedure);
                return id;
        }
        #endregion

    }
}
