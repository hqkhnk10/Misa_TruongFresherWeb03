using AutoMapper;
using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.DL.Repository.EmisStudy.QuestionRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Misa_TruongWeb03.Common.Enum.EmisStudy.EmisStudyEnum;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.QuestionService
{
    public class QuestionService : BaseService<Question, QuestionGetDTO, QuestionPostDTO, QuestionPutDTO>, IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        #region Constructor
        public QuestionService(IQuestionRepository questionRepository, IMapper mapper) : base(questionRepository, mapper)
        {
            _questionRepository = questionRepository;
        }
        #endregion
        #region Method
        /// <summary>
        /// Thêm câu hỏi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<BaseEntity> Post(QuestionPostDTO model)
        {
            var valid = ValidateQuestion(model);
            if (valid.Data == false)
            {
                return new BaseEntity
                {
                    ErrorCode = StatusCodes.Status400BadRequest,
                    DevMsg = valid.Message,
                    UserMsg = valid.Message
                };
            }
            var result = await _questionRepository.Post(model, model.Exercise.ExerciseId);
            return result;
        }
        /// <summary>
        /// Sửa câu hỏi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<BaseEntity> Put(int id, QuestionPostDTO model)
        {
            var valid = ValidateQuestion(model);
            if (valid.Data == false)
            {
                return new BaseEntity
                {
                    ErrorCode = StatusCodes.Status400BadRequest,
                    DevMsg = valid.Message,
                    UserMsg = valid.Message
                };
            }
            var result = await _questionRepository.Put(id, model);
            return result;
        }

        private ValidationResponse ValidateQuestion(QuestionPostDTO model)
        {
            switch (model.QuestionType)
            {
                case QuestionType.Choosing:
                    return ValidateChoosingAnswer(model.Answers);
                case QuestionType.TrueOrFalse:
                    return ValidateTrueOrFalseAnswer(model.Answers);
                default:
                    return new ValidationResponse { Data = true };
            }
        }
        private ValidationResponse ValidateChoosingAnswer(List<AnswerPostModel> model)
        {
            if (model.Count < 1) return new ValidationResponse { Data = false, Message = "Phải có ít nhất 1 đáp án" };
            if (!model.Contains(new AnswerPostModel { AnswerStatus = true })) return new ValidationResponse { Data = false, Message = "Phải có ít nhất 1 đáp án đúng" };
            return new ValidationResponse { Data = true };
        }
        private ValidationResponse ValidateTrueOrFalseAnswer(List<AnswerPostModel> model)
        {
            if (model.Count != 2) return new ValidationResponse { Data = false, Message = "Phải có 2 đáp án" };
            if (model.FindAll(m => m.AnswerStatus == true).Count != 1) return new ValidationResponse { Data = false, Message = "Chỉ được chọn 1 đáp án đúng" };
            return new ValidationResponse { Data = true };

        }
        #endregion
    }
}
