using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using AutoMapper;
using Misa_TruongWeb03.Common.Entity.Base;

namespace Misa_TruongWeb03.BL.Automapper
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<AnswerPostModel, Answer>();
        }
    }
}
