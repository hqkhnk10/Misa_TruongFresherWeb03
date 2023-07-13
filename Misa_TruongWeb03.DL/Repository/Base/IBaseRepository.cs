using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.DL.Entity.Base;
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
    public interface IBaseRepository<T>
    {
        #region Method
        Task<(IEnumerable<T>,int)> Get(Dictionary<string, object> dictionary, FilterModel filter, string? sort);
        Task<T> GetById(Guid id);
        Task<Guid> Post(T model);
        Task<int> Put(Guid id, T model);
        Task<int> Delete(Guid id);
        Task<bool> CheckDuplicate(T model); 
        #endregion

    }
}
