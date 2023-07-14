using AutoMapper;
using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.BL.Service.EmisStudy.ExerciseService;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.DL.Entity.Base;
using Misa_TruongWeb03.DL.Repository.EmisStudy.AnswerRepo;
using Misa_TruongWeb03.DL.Repository.EmisStudy.ExerciseRepo;
using Misa_TruongWeb03.DL.Repository.EmisStudy.QuestionRepo;
using Misa_TruongWeb03.DL.Repository.UnitOfWorkk;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Misa_TruongWeb03.Common.Enum.EmisStudy.EmisStudyEnum;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.QuestionService
{
    public class QuestionService : BaseService<Question, Question, QuestionDTO, QuestionPostDTO, QuestionPostDTO, QuestionPutDTO>, IQuestionService
    {
        #region Property
        private readonly IExerciseService _exerciseService;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constructor
        public QuestionService(IExerciseService exerciseService, IQuestionRepository questionRepository, IAnswerRepository answerRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(questionRepository, mapper, unitOfWork)
        {
            _exerciseService = exerciseService;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Method
        /// <summary>
        /// Thêm câu hỏi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// CreatedBy: NQTruong (01/07/2023)
        public override async Task<Guid> Post(QuestionPostDTO model)
        {

            var valid = ValidateQuestion(model.QuestionType, model.Answers);
            if (valid.Data == false)
            {
                throw new BaseException
                {
                    ErrorCode = StatusCodes.Status400BadRequest,
                    ErrorMsg = valid.Message
                };
            }
            var questionEntity = _mapper.Map<Question>(model);
            var answerEntity = _mapper.Map<List<Answer>>(model.Answers);

            _unitOfWork.BeginTransaction();
            try
            {
                var exerciseId = await _exerciseService.AddOrUpdate(model.Exercise);
                questionEntity.ExerciseId = exerciseId;
                var questionId = await _questionRepository.Post(questionEntity);
                if (answerEntity.Count > 0)
                {
                    var result = await _answerRepository.PostMultiple(questionId, exerciseId, answerEntity);
                }
                _unitOfWork.Commit();
                return exerciseId;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new InternalException(ex);
            }

        }
        /// <summary>
        /// Sửa câu hỏi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// CreatedBy: NQTruong (01/07/2023)
        public override async Task<Guid> Put(Guid id, QuestionPutDTO model)
        {

            var valid = ValidateQuestion(model.QuestionType, model.Answers);
            if (valid.Data == false)
            {
                throw new BaseException
                {
                    ErrorCode = StatusCodes.Status400BadRequest,
                    ErrorMsg = valid.Message
                };
            }

            var questionEntity = _mapper.Map<Question>(model);
            var answerEntity = _mapper.Map<List<Answer>>(model.Answers);
            _unitOfWork.BeginTransaction();
            try
            {
                var exerciseId = await _exerciseService.AddOrUpdate(model.Exercise);
                await _answerRepository.DeleteMultiple(id);
                questionEntity.ExerciseId = exerciseId;
                await _questionRepository.Put(id, questionEntity);
                var result = await _answerRepository.PostMultiple(id, exerciseId, answerEntity);
                _unitOfWork.Commit();
                return id;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new InternalException(ex);
            }

        }


        /// <summary>
        /// Validate dữ liệu câu hỏi theo từng loại
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// CreatedBy: NQTruong (01/07/2023)
        private ValidationResponse ValidateQuestion(QuestionType QuestionType, List<AnswerPostDTO> Answers)
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
        private ValidationResponse ValidateFillAnswer(List<AnswerPostDTO> model)
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
        private ValidationResponse ValidateChoosingAnswer(List<AnswerPostDTO> model)
        {
            if (model.Count < 1) return new ValidationResponse { Data = false, Message = "Phải có ít nhất 1 đáp án" };
            if (model.All(answer => answer.AnswerStatus == false))
            {
                return new ValidationResponse { Data = false, Message = "Phải có ít nhất 1 đáp án đúng" };
            }
            return new ValidationResponse { Data = true };
        }
        /// <summary>
        /// Validate dữ liệu câu hỏi đúng sai
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// CreatedBy: NQTruong (01/07/2023)
        private ValidationResponse ValidateTrueOrFalseAnswer(List<AnswerPostDTO> model)
        {
            if (model.Count != 2) return new ValidationResponse { Data = false, Message = "Phải có 2 đáp án" };
            if (model.FindAll(m => m.AnswerStatus == true).Count != 1) return new ValidationResponse { Data = false, Message = "Chỉ được chọn 1 đáp án đúng" };
            return new ValidationResponse { Data = true };

        }
        #endregion
    }
}
