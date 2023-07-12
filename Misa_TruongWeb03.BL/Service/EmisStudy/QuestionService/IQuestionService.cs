using AutoMapper;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.DL.Repository.EmisStudy.QuestionRepo;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.QuestionService
{
    public interface IQuestionService : IBaseService<Question, QuestionDTO, QuestionPostDTO, QuestionPostDTO,QuestionPutDTO>
    {

    }
}