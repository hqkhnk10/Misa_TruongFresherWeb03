using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.BL.Service.Base
{
    public interface IBaseService<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto>
    {
        Task<BaseEntity> Get(TEntityGetDto model);
        Task<BaseEntity> GetDetail(int id);
        Task<BaseEntity> Post(TEntityPostDto model);
        Task<BaseEntity> Put(int id, TEntityPostDto model);
        Task<BaseEntity> Delete(int id);
        Task<BaseEntity> CheckDuplicate(TEntity model);

    }
}
