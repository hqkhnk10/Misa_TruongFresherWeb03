using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Topic;
using Misa_TruongWeb03.DL.Repository.Base;
using Misa_TruongWeb03.DL.Repository.UnitOfWorkk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.TopicRepo
{
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        #region Constructor
        public TopicRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
        {
        }
        #endregion
    }
}
