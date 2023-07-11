using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using AutoMapper;
using Misa_TruongWeb03.Common.Entity.Base;

namespace Misa_TruongWeb03.BL.Automapper
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<ExerciseGetDTO, Exercise>();
            CreateMap<ExerciseGetDTO, FilterModel>();
            CreateMap<ExercisePostDTO, Exercise>();
        }
    }
}
