using AutoMapper;
using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Entity.Base;
using Misa_TruongWeb03.DL.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public abstract class BaseService<TEntity,TEntityDto, TEntityGetDto, TEntityPostDto, TEntityPutDto> : IBaseService<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto>
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
        public virtual async Task<GetResponse> Get(TEntityGetDto model, FilterModel filter)
        {
            try
            {
                var dictionary = new Dictionary<string, object>();

                // Iterate over the properties using reflection
                foreach (var property in model.GetType().GetProperties())
                {
                    // Get the property name and value
                    string propertyName = property.Name;
                    object propertyValue = property.GetValue(model);

                    // Add the mapping to the dictionary
                    dictionary.Add(propertyName, propertyValue);
                }

                var result = await _baseRepository.Get(dictionary, filter);
                
                return new GetResponse
                {
                    Data = result.Data,
                    Pagination = result.Pagination,
                };
            }
            catch (Exception ex)
            {
                throw new InternalException(ex);
            }
        }
        /// <summary>
        /// BASE GET Detail call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<TEntityDto> GetDetail(Guid id)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    throw new NotFoundException();
                }
                return _mapper.Map<TEntityDto>(result);
            }
            catch (Exception ex)
            {
                throw new InternalException(ex);
            }
        }
        /// <summary>
        /// BASE Post call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<Guid> Post(TEntityPostDto model)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                var result = await _baseRepository.Post(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new InternalException(ex);
            }
        }
        /// <summary>
        /// BASE PUT call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<Guid> Put(Guid id, TEntityPutDto model)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                var result = await _baseRepository.Put(id, entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new InternalException(ex);
            }
        }
        /// <summary>
        /// BASE DELETE call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _baseRepository.Delete(id);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new InternalException(ex);
            }
        }
        /// <summary>
        /// BASE check duplicate to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<bool> CheckDuplicate(TEntity model)
        {
            try
            {
                var result = await _baseRepository.CheckDuplicate(model);
                if (result)
                {
                    throw new DuplicateException();
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new InternalException(ex);
            }
        }
        #endregion
    }
}
