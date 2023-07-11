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
        public virtual async Task<ServiceResponse> Get(TEntityGetDto model)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                var getModel = _mapper.Map<FilterModel>(model);
                var result = await _baseRepository.Get(entity, getModel);
                return new ServiceResponse
                {
                    Data = result.Data,
                    Pagination = result.Pagination,
                };
            }
            catch (Exception ex)
            {
                return new ExceptionError(ex);
            }
        }
        /// <summary>
        /// BASE GET Detail call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<ServiceResponse> GetDetail(Guid id)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new NotFoundError();
                }
                return new ServiceResponse
                {
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new ExceptionError(ex);
            }
        }
        /// <summary>
        /// BASE Post call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<ServiceResponse> Post(TEntityPostDto model)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                var result = await _baseRepository.Post(entity);
                return new ServiceResponse
                {
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new ExceptionError(ex);
            }
        }
        /// <summary>
        /// BASE PUT call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<ServiceResponse> Put(Guid id, TEntityPutDto model)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                var result = await _baseRepository.Put(id, entity);
                return new ServiceResponse
                {
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new ExceptionError(ex);
            }
        }
        /// <summary>
        /// BASE DELETE call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<ServiceResponse> Delete(Guid id)
        {
            try
            {
                var result = await _baseRepository.Delete(id);
                return new ServiceResponse
                {
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new ExceptionError(ex);
            }
        }
        /// <summary>
        /// BASE check duplicate to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<ServiceResponse> CheckDuplicate(TEntity model)
        {
            try
            {
                var result = await _baseRepository.CheckDuplicate(model);
                if (result)
                {
                    return new DuplicateError();
                }
                return new ServiceResponse
                {
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new ExceptionError(ex);
            }
        }
        #endregion
    }
}
