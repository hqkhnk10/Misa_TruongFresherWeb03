using Dapper;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Repository.Base
{
    public interface IBaseRepository<T, TGetDTO, TPostDTO, TPutDTO>
    {
        DbConnection GetConnection();
        Task<BaseEntity> Get(TGetDTO model);
        Task<BaseEntity> GetById(int id);
        Task<BaseEntity> Post(TPostDTO model);
        Task<BaseEntity> Put(TPutDTO model);
        Task<BaseEntity> Delete(int id);
        Task<BaseEntity> CheckDuplicate(T model);

    }
}
