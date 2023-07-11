using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.DL.Repository.Base;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.QuestionRepo
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        Task<Guid> Post(Question model, Guid? ExerciseId);
        Task<Guid> Put(Guid questionId,Guid excerciseId, Question model);

    }
}