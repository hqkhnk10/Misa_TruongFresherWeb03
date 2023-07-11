using AutoMapper;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.DL.Repository.EmisStudy.ExerciseRepo;
using System.Text.Json;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.ExerciseService
{
    public class ExerciseService : BaseService<Exercise, ExerciseGetDTO, ExercisePostDTO, ExercisePutDTO>, IExerciseService
    {
        #region Property
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        #endregion
        #region Constructor
        public ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper) : base(exerciseRepository, mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }
        #endregion
        #region Method
        public override async Task<ServiceResponse> GetDetail(Guid id)
        {
            try
            {
                var result = await _exerciseRepository.GetDetail(id);
                if (result == null)
                {
                    return new NotFoundError();
                }
                return new ServiceResponse
                {
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new ExceptionError(ex);
            }
        }
        public async Task<Guid> AddOrUpdate(ExercisePostDTO model)
        {
            var entity = _mapper.Map<Exercise>(model);
            if (model.ExerciseId is null)
            {
                return await _exerciseRepository.Post(entity);
            }
            else
            {
                await _exerciseRepository.Put((Guid)model.ExerciseId, entity);
                return (Guid)model.ExerciseId;
            }
        }
        #endregion
    }
}
