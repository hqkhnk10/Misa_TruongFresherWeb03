using AutoMapper;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.DL.Repository.EmisStudy.QuestionRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.QuestionService
{
    public class QuestionService : BaseService<Question, QuestionGetDTO, QuestionPostDTO, QuestionPutDTO>, IQuestionService
    {
        #region Constructor
        public QuestionService(IQuestionRepository questionRepository, IMapper mapper) : base(questionRepository, mapper)
        {
        }
        #endregion
    }
}
