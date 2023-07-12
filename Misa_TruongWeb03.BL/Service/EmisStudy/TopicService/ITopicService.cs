using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Topic;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.TopicService
{
    public interface ITopicService : IBaseService<Topic, Topic, TopicGetDTO, TopicPostDTO, TopicPutDTO>
    {
    }
}