using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Supject;
using Misa_TruongWeb03.DL.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.SubjectRepo
{
    public class SubjectRepository : BaseRepository<Subject,SubjectGetDTO, SubjectPostDTO, SubjectPutDTO>, ISubjectRepository
    {
        #region Constructor
        public SubjectRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
    }
}
