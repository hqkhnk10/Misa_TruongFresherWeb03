﻿using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using Misa_TruongWeb03.DL.Repository.Base;
using System.Data.Common;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.AnswerRepo
{
    public interface IAnswerRepository : IBaseRepository<Answer,Answer>
    {
        Task<int> PostMultiple(Guid questionId, Guid exerciseId, List<Answer> answerList);
        Task<int> DeleteMultiple(Guid questionId);
    }
}