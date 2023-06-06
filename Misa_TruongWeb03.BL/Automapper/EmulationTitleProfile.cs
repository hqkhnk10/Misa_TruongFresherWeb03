using AutoMapper;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.EmulationTitle;

namespace Misa_TruongWeb03.BL.Automapper
{
    /// <summary>
    /// Mapper của danh hiểu thi đua
    /// </summary>
    /// Created By: NQTruong (10/05/2023)
    public class EmulationTitleProfile : Profile
    {
        public EmulationTitleProfile()
        { 
            CreateMap<PostEmulationTitle, EmulationTitle>();
            CreateMap<PostEmulationTitle, UpdateEmulationTitle>();
            CreateMap<EmulationTitle, UpdateEmulationTitle>();
        }
    }
}
