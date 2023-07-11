using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using Misa_TruongWeb03.DL.Repository.Base;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.AnswerRepo
{
    public interface IAnswerRepository : IBaseRepository<Answer>
    {
        Task<int> PostMultiple(Guid questionId, Guid exerciseId, List<Answer> answerList);
        Task<int> DeleteMultiple(Guid questionId);
    }
}