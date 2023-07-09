using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using Misa_TruongWeb03.DL.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.AnswerRepo
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        #region Constructor
        public AnswerRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
    }
}
