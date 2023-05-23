using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.EmulationTitleService;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity;
using Misa_TruongWeb03.Controller.Base;
using System.Reflection;
using System.Web.Http.Results;
using static Dapper.SqlMapper;

namespace FresherWeb03.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmulationTitleController : BaseController<EmulationTitle, GetEmulationTitle, PostEmulationTitle, UpdateEmulationTitle>
    {
        private readonly IEmulationTitleService _emulationTitleService;

        public EmulationTitleController(IEmulationTitleService emulationTitleService) : base(emulationTitleService)
        {
            _emulationTitleService = emulationTitleService;
        }
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
    }
}
