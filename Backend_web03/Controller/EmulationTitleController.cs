using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.EmulationTitle;
using Misa_TruongWeb03.Common.DTO;
using System.Reflection;
using System.Web.Http.Results;

namespace FresherWeb03.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmulationTitleController : ControllerBase
    {
        private readonly IEmulationTitleService _emulationTitleService;

        public EmulationTitleController(IEmulationTitleService emulationTitleService)
        {
            _emulationTitleService = emulationTitleService;
        }

        // GET 
        /// <summary>
        /// Lấy danh sách danh hiệu thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetEmulationTitle model)
        {
            try
            {
                var result = await _emulationTitleService.Get(model);
                return StatusCode(result.StatusCode, result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        // GET 
        /// <summary>
        /// Lấy chi tiết danh sách danh hiệu thi đua
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            try
            {
                var result = await _emulationTitleService.GetDetail(id);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<EmulationTitleController>
        /// <summary>
        /// Thêm phong trào thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostEmulationTitle model)
        {
            try
            {
                var result = await _emulationTitleService.Post(model);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // PUT api/<EmulationTitleController>/5
        /// <summary>
        /// Sửa phong trào thi đua
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PostEmulationTitle model)
        {
            try
            {
                var result = await _emulationTitleService.Put(id, model);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // DELETE api/<EmulationTitleController>/5
        /// <summary>
        /// Xóa 1 phong trào thi đua
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _emulationTitleService.Delete(id);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<EmulationTitleController>/5
        /// <summary>
        /// Xóa nhiều phong trào thi đua 1 lúc
        /// </summary>
        /// <param name="model">List id cần xóa</param>
        /// <returns></returns>
        [HttpDelete, Route("Multiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] DeleteEmulationTitle model)
        {
            try
            {
                var result = await _emulationTitleService.DeleteMultiple(model);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
