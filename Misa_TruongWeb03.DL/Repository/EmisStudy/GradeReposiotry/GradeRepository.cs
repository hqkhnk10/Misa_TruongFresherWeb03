using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Grade;
using Misa_TruongWeb03.DL.Repository.Base;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.GradeReposiotry
{
    public class GradeRepository : BaseRepository<Grade>, IGradeRepository
    {
        #region Constructor
        public GradeRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
    }
}
