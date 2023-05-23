using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using System.Web.Http.ModelBinding;

namespace Misa_TruongWeb03.Controller.Base
{
    [Route("api/v1/[controller]")]
    public class BaseController<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto> : ControllerBase
    {
        protected readonly IBaseService<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto> _baseService;

        public BaseController(IBaseService<TEntity, TEntityGetDto, TEntityPostDto, TEntityPutDto> baseService)
        {
            _baseService = baseService;
        }
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
                UserMsg = "Validation failed.",
                DevMsg = errors.ToString() ?? ""
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
                UserMsg = "Lỗi hệ thống",
            };
            return StatusCode(500, response);
        }
        #endregion
    }
}
