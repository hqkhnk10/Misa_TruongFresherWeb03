using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Grade;
using AutoMapper;
using Misa_TruongWeb03.Common.Entity.Base;

namespace Misa_TruongWeb03.BL.Automapper
{
    public class GradeProfile : Profile
    {
        public GradeProfile()
        {
            CreateMap<GradeGetDTO, Grade>();
        }
    }
}
