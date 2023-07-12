using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using AutoMapper;
using Misa_TruongWeb03.Common.Entity.Base;

namespace Misa_TruongWeb03.BL.Automapper
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionPostDTO, Question>();
            CreateMap<Question, QuestionDTO>();
        }
    }
}
