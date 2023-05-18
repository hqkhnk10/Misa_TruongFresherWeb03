using AutoMapper;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;

namespace Misa_TruongWeb03.BL.Automapper
{
    public class EmulationTitleProfile : Profile
    {
        public EmulationTitleProfile()
        {
            CreateMap<PostEmulationTitle, EmulationTitleModel>();
            CreateMap<EmulationTitleModel, PostEmulationTitle>();
        }
    }
}
