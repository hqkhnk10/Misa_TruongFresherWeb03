﻿using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.Common.Resource;
using System.Web.Http.ModelBinding;

namespace Misa_TruongWeb03.Controller.Base
{
    /// <summary>
    /// Base Controller kết nối với tầng Base Service
    /// </summary>
    /// <typeparam name="TEntity">Generic Entity model</typeparam>
    /// <typeparam name="TEntityGetDto">Generic Get DTO model</typeparam>
    /// <typeparam name="TEntityPostDto">Generic Post DTO model</typeparam>
    /// <typeparam name="TEntityPutDto">Generic Put DTO model</typeparam>
    /// CreatedBy: QTNgo (24/05/2023)
    [Route("api/v1/[controller]")]
    public class BaseController<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto> : ControllerBase
    {
        #region Property
        protected readonly IBaseService<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto> _baseService;
        #endregion

        #region Constructor
        public BaseController(IBaseService<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto> baseService)
        {
            _baseService = baseService;
        }
        #endregion
        #region Method
        /// <summary>
        /// BASE GET
        /// </summary>
        /// <param name="model"></param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TEntityGetDto model)
        {
            if (!ModelState.IsValid)
            {
                return HandleValidationErrors();
            }
            try
            {
                var result = await _baseService.Get(model);
                return StatusCode(result.ErrorCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        /// <summary>
        /// BASE Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            try
            {
                var result = await _baseService.GetDetail(id);
                return StatusCode(result.ErrorCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// BASE POST
        /// </summary>
        /// <param name="model"></param>
        /// <returns>IActionResult</returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TEntityPostDto model)
        {
            if (!ModelState.IsValid)
            {
                return HandleValidationErrors();
            }
            try
            {
                var result = await _baseService.Post(model);
                return StatusCode(result.ErrorCode, result);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }
        /// <summary>
        /// BASE PUT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TEntityPostDto model)
        {
            if (!ModelState.IsValid)
            {
                return HandleValidationErrors();
            }
            try
            {
                var result = await _baseService.Put(id, model);
                return StatusCode(result.ErrorCode, result);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }
        /// <summary>
        /// BASE DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")] 
        #endregion
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _baseService.Delete(id);
                return StatusCode(result.ErrorCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #region Event
        /// <summary>
        /// Trả về lỗi validate
        /// </summary>
        /// <returns></returns>
        protected IActionResult HandleValidationErrors()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var response = new BaseEntity
            {
                ErrorCode = StatusCodes.Status400BadRequest,
                UserMsg = VN.ValidationError,
                DevMsg = errors.ToString() ?? VN.Error
            };

            return BadRequest(response);
        }
        /// <summary>
        /// Trả về lỗi hệ thống
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected IActionResult ServerError(Exception ex)
        {
            var response = new BaseEntity
            {
                ErrorCode = StatusCodes.Status500InternalServerError,
                DevMsg = ex.Message,
                UserMsg = VN.Error500,
            };
            return StatusCode(500, response);
        }
        #endregion
    }
}