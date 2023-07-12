using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.DL.Repository.Base;
using System.Data.Common;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.ExerciseRepo
{
    public interface IExerciseRepository : IBaseRepository<Exercise>
    {
        Task<IEnumerable<DetailExerciseModel>> GetDetail(Guid Id);
        Task<Guid> Post(Exercise model, DbTransaction transaction);
        Task<Guid> Put(Guid exerciseId ,Exercise model, DbTransaction transaction);
    }
}