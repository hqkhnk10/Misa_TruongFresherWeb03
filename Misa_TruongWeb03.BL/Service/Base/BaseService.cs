using AutoMapper;
using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Entity.Base;
using Misa_TruongWeb03.DL.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.BL.Service.Base
{
    /// <summary>
    /// Base Service kết nối với tầng base repository
    /// </summary>
    /// <typeparam name="TEntity">Generic Entity model</typeparam>
    /// <typeparam name="TEntityGetDto">Generic Get DTO model</typeparam>
    /// <typeparam name="TEntityPostDto">Generic Post DTO model</typeparam>
    /// <typeparam name="TEntityPutDto">Generic Put DTO model</typeparam>
    /// CreatedBy: NQTruong (24/05/2023)
    public abstract class BaseService<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto> : IBaseService<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto>
    {
        #region Property
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        #endregion
        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        #endregion
        #region MyRegion
        /// <summary>
        /// BASE GET call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<BaseGet<IEnumerable<TEntity>>> Get(TEntityGetDto model)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                var getModel = _mapper.Map<FilterModel>(model);
                var result = await _baseRepository.Get(entity, getModel);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// BASE GET Detail call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<TEntity> GetDetail(Guid id)
        {
            var result = await _baseRepository.GetById(id);
            if(result.Data == null)
            {
                return new BaseEntity
                {
                    Data = result.Data,
                    ErrorCode = StatusCodes.Status404NotFound
                };
            }
            return result;
        }
        /// <summary>
        /// BASE Post call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<Guid> Post(TEntityPostDto model)
        {
            var entity = _mapper.Map<TEntity>(model);
            var result = await _baseRepository.Post(entity);
            return result;
        }
        /// <summary>
        /// BASE PUT call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<int> Put(Guid id, TEntityPutDto model)
        {
            var entity = _mapper.Map<TEntity>(model);
            var result = await _baseRepository.Put(id, entity);
            return result;
        }
        /// <summary>
        /// BASE DELETE call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<int> Delete(Guid id)
        {
            var result = await _baseRepository.Delete(id);
            return result;
        }
        /// <summary>
        /// BASE check duplicate to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<bool> CheckDuplicate(TEntity model)
        {
            return await _baseRepository.CheckDuplicate(model);
        } 
        #endregion
    }
}
