using Misa_TruongWeb03.Common.Entity;
using System.Data.Common;

namespace Misa_TruongWeb03.DL.Repository.Base
{
    /// <summary>
    /// Base Interface khai báo các hàm của lớp base repository
    /// </summary>
    /// <typeparam name="T">Generic Entity model</typeparam>
    /// <typeparam name="TGetDTO">Generic Get DTO model</typeparam>
    /// <typeparam name="TPostDTO">Generic Post DTO model</typeparam>
    /// <typeparam name="TPutDTO">Generic Put DTO model</typeparam>
    /// CreatedBy: NQTruong (24/05/2023)
    public interface IBaseRepository<T, TGetDTO, TPostDTO, TPutDTO>
    {
        #region Method
        DbConnection GetConnection();
        Task<BaseEntity> Get(TGetDTO model);
        Task<BaseEntity> GetById(int id);
        Task<BaseEntity> Post(TPostDTO model);
        Task<BaseEntity> Put(TPutDTO model);
        Task<BaseEntity> Delete(int id);
        Task<BaseEntity> CheckDuplicate(T model); 
        #endregion

    }
}
