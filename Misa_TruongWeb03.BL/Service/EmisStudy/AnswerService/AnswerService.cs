using AutoMapper;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using Misa_TruongWeb03.DL.Repository.EmisStudy.AnswerRepo;
using Misa_TruongWeb03.DL.Repository.UnitOfWorkk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.AnswerService
{
    public class AnswerService : BaseService<Answer, Answer, AnswerDTO, AnswerGetDTO, AnswerPostDTO, AnswerPutDTO>, IAnswerService
    {
        #region Constructor
        public AnswerService(IAnswerRepository answerRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(answerRepository, mapper, unitOfWork)
        {
        }
        #endregion
    }
}
