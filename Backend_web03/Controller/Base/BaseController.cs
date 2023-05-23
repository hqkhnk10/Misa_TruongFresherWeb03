using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.Common.DTO;
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
        // GET 
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
        // GET 
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
                throw new Exception(ex.Message);
            }
        }
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
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
        protected IActionResult HandleValidationErrors()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var response = new
            {
                Message = "Validation failed.",
                Errors = errors
            };

            return BadRequest(response);
        } 
        #endregion
    }
}
