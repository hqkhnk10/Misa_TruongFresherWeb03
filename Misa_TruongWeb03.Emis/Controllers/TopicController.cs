using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.EmisStudy.TopicService;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Topic;
using Misa_TruongWeb03.Controller.Base;

namespace Misa_TruongWeb03.Emis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : BaseController<Topic, Topic, TopicGetDTO, TopicPostDTO, TopicPutDTO>
    {
        #region Constructor
        public TopicController(ITopicService topicService) : base(topicService)
        {
        }
        #endregion
    }
}
