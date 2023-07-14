using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.DL.Repository.Base;
using System.Data.Common;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.QuestionRepo
{
    public interface IQuestionRepository : IBaseRepository<Question, Question>
    {
       
    }
}