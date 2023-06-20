using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Grade;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.GradeService
{
    public interface IGradeService : IBaseService<Grade, GradeGetDTO, GradePostDTO, GradePutDTO>
    {
    }
}