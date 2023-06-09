﻿using AutoMapper;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Topic;
using Misa_TruongWeb03.DL.Repository.EmisStudy.AnswerRepo;
using Misa_TruongWeb03.DL.Repository.EmisStudy.TopicRepo;
using Misa_TruongWeb03.DL.Repository.UnitOfWorkk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.TopicService
{
    public class TopicService : BaseService<Topic, Topic, Topic, TopicGetDTO, TopicPostDTO, TopicPutDTO>, ITopicService
    {
        #region Constructor
        public TopicService(ITopicRepository topicRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(topicRepository, mapper, unitOfWork)
        {
        }
        #endregion
    }
}
