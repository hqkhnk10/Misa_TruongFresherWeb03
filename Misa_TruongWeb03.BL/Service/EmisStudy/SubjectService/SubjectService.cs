using AutoMapper;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Supject;
using Misa_TruongWeb03.DL.Repository.EmisStudy.SubjectRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.BL.Service.EmisStudy.SubjectService
{
    public class SubjectService : BaseService<Subject, Subject, SubjectGetDTO, SubjectPostDTO, SubjectPutDTO>, ISubjectService
    {
        #region Constructor
        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper) : base(subjectRepository, mapper)
        {
        }
        #endregion
    }
}
