using Dapper;
using Dapper.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Exercise;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Question;
using Misa_TruongWeb03.Common.Helper;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Repository.Base;
using Misa_TruongWeb03.DL.Repository.UnitOfWorkk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Repository.EmisStudy.QuestionRepo
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        #region Constructor
        public QuestionRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
        {
        }
        #endregion
    }
}
