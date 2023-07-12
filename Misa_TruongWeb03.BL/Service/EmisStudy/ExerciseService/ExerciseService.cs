using AutoMapper;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.DL.Entity.Base;
using Misa_TruongWeb03.DL.Repository.EmisStudy.AnswerRepo;
using Misa_TruongWeb03.DL.Repository.EmisStudy.ExerciseRepo;
using Misa_TruongWeb03.DL.Repository.EmisStudy.QuestionRepo;
using System.Data.Common;
using System.Text.Json;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.ExerciseService
{
    public class ExerciseService : BaseService<Exercise,ExerciseDTO, ExerciseGetDTO, ExercisePostDTO, ExercisePutDTO>, IExerciseService
    {
        #region Property
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        #endregion
        #region Constructor
        public ExerciseService(IExerciseRepository exerciseRepository,IQuestionRepository questionRepository, IAnswerRepository answerRepository, IMapper mapper) : base(exerciseRepository, mapper)
        {
            _exerciseRepository = exerciseRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _mapper = mapper;
        }
        #endregion
        #region Method

        protected override async Task GetDetailEntity(Guid id, ExerciseDTO entityDto)
        {
            var dict = new Dictionary<string, object>
            {
                { "ExerciseId", id }
            };
            var (questionData,quesiontCount) = await _questionRepository.Get(dict, new FilterModel());
            var (answerData,answerCount) =  await _answerRepository.Get(dict, new FilterModel());

            var answerDto = _mapper.Map<List<AnswerDTO>>(answerData);
            var questionDto = _mapper.Map<List<QuestionDTO>>(questionData);

            questionDto.ForEach(q =>
            {
                q.Answers.AddRange(answerDto.Where(a => a.QuestionId == q.QuestionId));
            });

            entityDto.Questions.AddRange(questionDto);
        }

        //public override async Task<ExerciseDTO> GetDetail(Guid id)
        //{
        //    try
        //    {
        //        var result = await _exerciseRepository.GetDetail(id);
        //        if (result == null)
        //        {
        //            return new NotFoundError();
        //        }
        //        return new ServiceResponse
        //        {
        //            Data = result,
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ExceptionError(ex);
        //    }
        //}
        //public async Task<Guid> AddOrUpdate(ExercisePostDTO model, DbTransaction transaction)
        //{
        //    var entity = _mapper.Map<Exercise>(model);
        //    if (model.ExerciseId is null)
        //    {
        //        return await _exerciseRepository.Post(entity, transaction);
        //    }
        //    else
        //    {
        //        await _exerciseRepository.Put((Guid)model.ExerciseId, entity, transaction);
        //        return (Guid)model.ExerciseId;
        //    }
        //}
        #endregion
    }
}
