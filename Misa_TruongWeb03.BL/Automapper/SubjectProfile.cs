using Misa_TruongWeb03.Common.DTO.EmisStudy;
using AutoMapper;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Supject;

namespace Misa_TruongWeb03.BL.Automapper
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<SubjectGetDTO, Subject>();
        }
    }
}
