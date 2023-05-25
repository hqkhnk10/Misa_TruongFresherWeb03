using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.EmulationTitleService;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.Controller.Base;

namespace FresherWeb03.Controller
{
    /// <summary>
    /// Danh hiệu thi đua Controller
    /// kế thừa các CRUD từ Base Controller
    /// </summary>
    /// CreatedBy: QTNgo (24/05/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmulationTitleController : BaseController<EmulationTitle, GetEmulationTitle, PostEmulationTitle, UpdateEmulationTitle>
    {
        #region Property
        private readonly IEmulationTitleService _emulationTitleService; 
        #endregion
        #region Constructor
        public EmulationTitleController(IEmulationTitleService emulationTitleService) : base(emulationTitleService)
        {
            _emulationTitleService = emulationTitleService;
        }
        #endregion
        #region Method
        // DELETE api/<EmulationTitleController>/5
        /// <summary>
        /// Xóa nhiều phong trào thi đua 1 lúc
        /// </summary>
        /// <param name="model">List id cần xóa</param>
        /// <returns></returns>
        /// Created By: QTNgo (23/05/2023)
        [HttpDelete, Route("Multiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] DeleteEmulationTitle model)
        {
            if (!ModelState.IsValid)
            {
                return HandleValidationErrors();
            }
            else
            {
                try
                {
                    var result = await _emulationTitleService.DeleteMultiple(model);
                    return StatusCode(result.ErrorCode, result);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        // DELETE api/<EmulationTitleController>/5
        /// <summary>
        /// Xóa nhiều phong trào thi đua 1 lúc
        /// </summary>
        /// <param name="model">List id cần xóa</param>
        /// <returns></returns>
        /// Created By: QTNgo (23/05/2023)
        [HttpPut, Route("Status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateEmulationTitleStatusDto model)
        {
            if (!ModelState.IsValid)
            {
                return HandleValidationErrors();
            }
            else
            {
                try
                {
                    var result = await _emulationTitleService.UpdateStatus(model);
                    return StatusCode(result.ErrorCode, result);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        // DELETE api/<EmulationTitleController>/5
        /// <summary>
        /// Xóa nhiều phong trào thi đua 1 lúc
        /// </summary>
        /// <param name="model">List id cần xóa</param>
        /// <returns></returns>
        /// Created By: QTNgo (23/05/2023)
        [HttpPut, Route("Mulitple")]
        public async Task<IActionResult> UpdateMultipleStatus(UpdateMultipleEmulationTitleStatusDto model)
        {
            if (!ModelState.IsValid)
            {
                return HandleValidationErrors();
            }
            else
            {
                try
                {
                    var result = await _emulationTitleService.UpdateMultipleStatus(model);
                    return StatusCode(result.ErrorCode, result);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }
        #endregion
    }
}
