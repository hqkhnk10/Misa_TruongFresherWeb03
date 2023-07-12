using Misa_TruongWeb03.Common.DTO.EmisStudy;
using AutoMapper;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Topic;

namespace Misa_TruongWeb03.BL.Automapper
{
    public class TopicProfile : Profile
    {
        public TopicProfile()
        {
            CreateMap<TopicGetDTO, Topic>();
        }
    }
}
