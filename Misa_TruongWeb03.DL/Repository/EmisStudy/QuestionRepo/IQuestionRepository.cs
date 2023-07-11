﻿using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.DL.Repository.Base;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.QuestionRepo
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        Task<BaseEntity> Post(QuestionPostDTO model, Guid? ExerciseId);
        Task<BaseEntity> Put(Guid id, QuestionPostDTO model);

    }
}