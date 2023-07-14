using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Supject;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.SubjectService
{
    public interface ISubjectService : IBaseService< Subject, SubjectGetDTO, SubjectPostDTO, SubjectPutDTO>
    {
    }
}