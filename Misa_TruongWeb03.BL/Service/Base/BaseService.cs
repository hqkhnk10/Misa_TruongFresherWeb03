using AutoMapper;
using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Resource;
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
        protected readonly IBaseRepository<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto> _baseRepository;
        protected readonly IMapper _mapper;
        #endregion
        #region Constructor
        public BaseService(IBaseRepository<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto> baseRepository, IMapper mapper)
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
        public async Task<BaseEntity> Get(TEntityGetDto model)
        {
            var result = await _baseRepository.Get(model);
            return result;
        }
        /// <summary>
        /// BASE GET Detail call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<BaseEntity> GetDetail(int id)
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
        public virtual async Task<BaseEntity> Post(TEntityPostDto model)
        {
            var result = await _baseRepository.Post(model);
            if (result.Data == null || (int)result.Data == 0)
            {
                return new DatabaseError();
            }
            return result;
        }
        /// <summary>
        /// BASE PUT call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<BaseEntity> Put(int id, TEntityPostDto model)
        {
            var entity = _mapper.Map<TEntityPutDto>(model);
            var result = await _baseRepository.Put(entity);
            if (result.Data == null || (int)result.Data == 0)
            {
                return new DatabaseError();
            }
            return result;
        }
        /// <summary>
        /// BASE DELETE call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<BaseEntity> Delete(int id)
        {
            var exist = await _baseRepository.GetById(id);
            if(exist.Data == null)
            {
                return new NotFoundError();
            }
            var result = await _baseRepository.Delete(id);
            if (result.Data == null || (int)result.Data == 0)
            {
                return new DatabaseError();
            }
            return result;
        }
        /// <summary>
        /// BASE check duplicate to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<BaseEntity> CheckDuplicate(TEntity model)
        {
            return await _baseRepository.CheckDuplicate(model);
        } 
        #endregion
    }
}
