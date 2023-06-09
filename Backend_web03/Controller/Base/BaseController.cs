﻿using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Resource;
using Misa_TruongWeb03.DL.Entity.Base;

namespace Misa_TruongWeb03.Controller.Base
{
    /// <summary>
    /// Base Controller kết nối với tầng Base Service
    /// </summary>
    /// <typeparam name="TEntity">Generic Entity model</typeparam>
    /// <typeparam name="TEntityGetDto">Generic Get DTO model</typeparam>
    /// <typeparam name="TEntityPostDto">Generic Post DTO model</typeparam>
    /// <typeparam name="TEntityPutDto">Generic Put DTO model</typeparam>
    /// CreatedBy: NQTruong (24/05/2023)
    [Route("api/v1/[controller]")]
    public abstract class BaseController<TEntity, TEntityDto, TEntityGetDto, TEntityPostDto, TEntityPutDto> : ControllerBase
    {
        #region Property
        protected readonly IBaseService<TEntityDto, TEntityGetDto, TEntityPostDto, TEntityPutDto> _baseService;
        #endregion
        #region Constructor
        public BaseController(IBaseService<TEntityDto, TEntityGetDto, TEntityPostDto, TEntityPutDto> baseService)
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
        public async Task<IActionResult> Get([FromQuery] TEntityGetDto model, [FromQuery] FilterModel filter, [FromQuery] string? Sort)
        {

            var result = await _baseService.Get(model, filter, Sort);
            return Ok(result);


        }
        /// <summary>
        /// BASE Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(Guid id)
        {

            var result = await _baseService.GetDetail(id);
            return Ok(result);

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

            var result = await _baseService.Post(model);
            return CreatedAtAction(nameof(this.Post),result);

        }
        /// <summary>
        /// BASE PUT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TEntityPutDto model)
        {

            var result = await _baseService.Put(id, model);
            return Ok(result);

        }
        /// <summary>
        /// BASE DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await _baseService.Delete(id);
            return Ok(result);

        }
        /// <summary>
        /// Trả về lỗi validate
        /// </summary>
        /// <returns></returns>
        protected IActionResult HandleValidationErrors()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var response = new
            {
                ErrorCode = StatusCodes.Status400BadRequest,
                UserMsg = string.Join(",", errors) ?? VN.ValidationError,
                DevMsg = string.Join(",", errors) ?? VN.ValidationError,
            };

            return BadRequest(response);
        }
        #endregion
    }
}
