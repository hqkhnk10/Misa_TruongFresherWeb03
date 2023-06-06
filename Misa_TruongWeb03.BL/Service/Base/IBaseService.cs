using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.BL.Service.Base
{
    /// <summary>
    /// Base Service khai báo các hàm của lớp base service
    /// </summary>
    /// <typeparam name="TEntity">Generic Entity model</typeparam>
    /// <typeparam name="TEntityGetDto">Generic Get DTO model</typeparam>
    /// <typeparam name="TEntityPostDto">Generic Post DTO model</typeparam>
    /// <typeparam name="TEntityPutDto">Generic Put DTO model</typeparam>
    /// CreatedBy: NQTruong (24/05/2023)
    public interface IBaseService<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto>
    {
        #region Method
        Task<BaseEntity> Get(TEntityGetDto model);
        Task<BaseEntity> GetDetail(int id);
        Task<BaseEntity> Post(TEntityPostDto model);
        Task<BaseEntity> Put(int id, TEntityPostDto model);
        Task<BaseEntity> Delete(int id);
        Task<BaseEntity> CheckDuplicate(TEntity model); 
        #endregion

    }
}
