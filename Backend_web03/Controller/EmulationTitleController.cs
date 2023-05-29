using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.EmulationTitleService;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.Controller.Base;
using Misa_TruongWeb03.Middleware;

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

        // put api/<EmulationTitleController>/STATUS
        /// <summary>
        ///  Thay đổi trạng thái 1 danh hiệu thi đua
        /// </summary>
        /// <param name="model">Id : Id của bản ghi, trạng thái thay đổi 1:Inactive, 0:Active</param>
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

        // PUT api/<EmulationTitleController>/MultipleStatus
        /// <summary>
        ///  Thay đổi trạng thái nhiều danh hiệu thi đua
        /// </summary>
        /// <param name="model">List id cần xóa, trạng thái thay đổi 1:Inactive, 0:Active</param>
        /// <returns></returns>
        /// Created By: QTNgo (23/05/2023)
        [HttpPut, Route("MulitpleStatus")]
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
