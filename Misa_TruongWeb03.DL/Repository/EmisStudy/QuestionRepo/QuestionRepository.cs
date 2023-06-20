using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.DL.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.QuestionRepo
{
    public class QuestionRepository : BaseRepository<Question, QuestionGetDTO, QuestionPostDTO, QuestionPutDTO>, IQuestionRepository
    {
        #region Constructor
        public QuestionRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
    }
}
