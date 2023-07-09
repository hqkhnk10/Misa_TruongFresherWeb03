using AutoMapper;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Grade;
using Misa_TruongWeb03.DL.Repository.EmisStudy.GradeReposiotry;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.GradeService
{
    public class GradeService : BaseService<Grade, GradeGetDTO, GradePostDTO, GradePutDTO>, IGradeService
    {
        #region Constructor
        public GradeService(IGradeRepository gradeRepository, IMapper mapper) : base(gradeRepository, mapper)
        {
        }
        #endregion
    }
}
