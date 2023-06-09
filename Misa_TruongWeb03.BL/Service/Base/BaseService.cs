﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Entity.Base;
using Misa_TruongWeb03.DL.Repository.Base;
using Misa_TruongWeb03.DL.Repository.UnitOfWorkk;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

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
    public abstract class BaseService<TEntity, TGetModel, TEntityDto, TEntityGetDto, TEntityPostDto, TEntityPutDto> : IBaseService<TEntityDto, TEntityGetDto, TEntityPostDto, TEntityPutDto>
    {
        #region Property
        protected readonly IBaseRepository<TEntity, TGetModel> _baseRepository;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constructor
        public BaseService(IBaseRepository<TEntity, TGetModel> baseRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Method
        /// <summary>
        /// BASE GET call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<GetResponse> Get(TEntityGetDto model, FilterModel filter, string sort)
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

            var (result, totalCount) = await _baseRepository.Get(dictionary, filter, sort);

            _unitOfWork.CloseConnection();

            return new GetResponse
            {
                Data = _mapper.Map<List<TEntityDto>>(result),
                Pagination = new Pagination
                {
                    Count = totalCount,
                    PageIndex = filter.PageIndex,
                    PageSize = filter.PageSize,
                }
            };

        }
        /// <summary>
        /// BASE GET Detail call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<TEntityDto> GetDetail(Guid id)
        {
            var result = await _baseRepository.GetById(id);
            if (result == null)
            {
                throw new NotFoundException();
            }
            var entityDto = _mapper.Map<TEntityDto>(result);
            await GetDetailEntity(id, entityDto);

            _unitOfWork.CloseConnection();

            return entityDto;

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

            var isValid = Validate(entity);
            if (!isValid)
            {
                throw new ValidateException();
            }

            var result = await _baseRepository.Post(entity);

            _unitOfWork.CloseConnection();

            return result;

        }
        /// <summary>
        /// BASE PUT call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public virtual async Task<Guid> Put(Guid id, TEntityPutDto model)
        {
            var entity = _mapper.Map<TEntity>(model);

            var isValid = Validate(entity);
            if (!isValid)
            {
                throw new ValidateException();
            }

            var exist = await _baseRepository.GetById(id);
            if (exist == null)
            {
                throw new NotFoundException();
            }
            var result = await _baseRepository.Put(id, entity);
            _unitOfWork.CloseConnection();
            if (result <= 0)
            {
                throw new DatabaseExeception();
            }
            return id;

        }
        /// <summary>
        /// BASE DELETE call to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        public async Task<bool> Delete(Guid id)
        {
            var exist = _baseRepository.GetById(id);
            if (exist == null)
            {
                throw new NotFoundException();
            }

            var result = await _baseRepository.Delete(id);
            _unitOfWork.CloseConnection();

            if (result <= 0)
            {
                throw new DatabaseExeception();
            }
            return true;

        }
        /// <summary>
        /// BASE check duplicate to BASE Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BaseEntity</returns>
        /// CreatedBy: NQTruong (24/05/2023)
        protected virtual bool Validate(TEntity model)
        {
            return true;
        }

        /// <summary>
        /// Override this to return more entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        protected virtual async Task GetDetailEntity(Guid id, TEntityDto entityDto)
        {
            await Task.Delay(0);
        }
        #endregion
    }
}
