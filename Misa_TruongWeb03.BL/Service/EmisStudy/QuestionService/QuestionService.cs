using AutoMapper;
using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.DL.Repository.EmisStudy.AnswerRepo;
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
        private readonly IMapper _mapper;
        #region Constructor
        public QuestionService(IQuestionRepository questionRepository, IAnswerRepository answerRepository, IMapper mapper) : base(questionRepository, mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        #endregion
        #region Method
        /// <summary>
        /// Thêm câu hỏi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// CreatedBy: NQTruong (01/07/2023)
        public override async Task<BaseEntity> Post(QuestionPostDTO model)
        {
            var valid = ValidateQuestion(model.QuestionType,model.Answers);
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
        /// CreatedBy: NQTruong (01/07/2023)
        public override async Task<BaseEntity> Put(int id, QuestionPostDTO model)
        {
            var valid = ValidateQuestion(model.QuestionType, model.Answers);
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

        /// <summary>
        /// Thêm câu hỏi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// CreatedBy: NQTruong (01/07/2023)
        //public async Task<BaseEntity> PostMultiple(List<QuestionImportDTO> model, int exceriseId)
        //{
        //    foreach (var question in model)
        //    {
        //        var questionEntity = _mapper.Map<Question>(question);
        //        var questionResult = await _questionRepository.Post(questionEntity, exceriseId);
        //        var result = await _questionRepository.Post(questionEntity, questionResult.Data);
        //    }
        //    return new BaseEntity();
        //}

        /// <summary>
        /// Validate dữ liệu câu hỏi theo từng loại
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// CreatedBy: NQTruong (01/07/2023)
        private ValidationResponse ValidateQuestion(QuestionType QuestionType, List<AnswerPostModel> Answers)
        {
            switch (QuestionType)
            {
                case QuestionType.Choosing:
                    return ValidateChoosingAnswer(Answers);
                case QuestionType.TrueOrFalse:
                    return ValidateTrueOrFalseAnswer(Answers);
                case QuestionType.Fill:
                    return ValidateFillAnswer(Answers);
                default:
                    return new ValidationResponse { Data = true };
            }
        }
        /// <summary>
        /// Validate dữ liệu câu hỏi điền vào chỗ trống
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// CreatedBy: NQTruong (01/07/2023)
        private ValidationResponse ValidateFillAnswer(List<AnswerPostModel> model)
        {
            if (model.Count < 1) return new ValidationResponse { Data = false, Message = "Phải có ít nhất 1 đáp án" };
            return new ValidationResponse { Data = true };
        }
        /// <summary>
        /// Validate dữ liệu câu hỏi chọn đáp án
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// CreatedBy: NQTruong (01/07/2023)
        private ValidationResponse ValidateChoosingAnswer(List<AnswerPostModel> model)
        {
            if (model.Count < 1) return new ValidationResponse { Data = false, Message = "Phải có ít nhất 1 đáp án" };
            if (!model.Contains(new AnswerPostModel { AnswerStatus = true })) return new ValidationResponse { Data = false, Message = "Phải có ít nhất 1 đáp án đúng" };
            return new ValidationResponse { Data = true };
        }
        /// <summary>
        /// Validate dữ liệu câu hỏi đúng sai
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// CreatedBy: NQTruong (01/07/2023)
        private ValidationResponse ValidateTrueOrFalseAnswer(List<AnswerPostModel> model)
        {
            if (model.Count != 2) return new ValidationResponse { Data = false, Message = "Phải có 2 đáp án" };
            if (model.FindAll(m => m.AnswerStatus == true).Count != 1) return new ValidationResponse { Data = false, Message = "Chỉ được chọn 1 đáp án đúng" };
            return new ValidationResponse { Data = true };

        }
        #endregion
    }
}
