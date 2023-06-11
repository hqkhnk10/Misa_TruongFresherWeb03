using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.FileServices;
using Misa_TruongWeb03.BL.Service.Import;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.FileEntity;
using Misa_TruongWeb03.Common.Helper;
using Misa_TruongWeb03.Common.DTO;

namespace Misa_TruongWeb03_File.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        #region Property
        private IServiceProvider _serviceProvider;
        private IFileService _fileService;
        #endregion
        #region Constructor
        public FileController(IServiceProvider serviceProvider, IFileService fileService)
        {
            _serviceProvider = serviceProvider;
            _fileService = fileService;
        }
        #endregion
        #region Method
        /// <summary>
        /// Single File Upload
        /// </summary>
        /// <param name="file"></param>
        /// Created By: NQTruong (01/06/2023)
        /// <returns></returns>
        [HttpPost("PostSingleFile")]
        public async Task<IActionResult> PostSingleFile(IFormFile file)
        {
            if (file == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError()); ;
            }
            try
            {
                var res = await _fileService.Upload(file);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionError(ex));
            }
        }
        /// <summary>
        /// Tải faile dựa trên tên file
        /// </summary>
        /// <param name="fileName"></param>
        /// Created By: NQTruong (01/06/2023)
        /// <returns></returns>
        [HttpGet("download")]
        public IActionResult Download(string fileName)
        {
            try
            {
                var fileData = _fileService.Download(fileName);
                return File(fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionError(ex));
            }
        }
        /// <summary>
        /// Xác thực dữ liệu file
        /// </summary>
        /// <param name="model"></param>
        /// Created By: NQTruong (01/06/2023)
        /// <returns></returns>
        [HttpPost("ValidateFile")]
        public async Task<IActionResult> ValidateFile([FromForm] ValidateFileDTO model)
        {
            var service = GetService(model.Key);
            if (service == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new NotFoundError()); ;
            }
            try
            {
                var res = await service.Validate(model.File, model.SheetIndex, model.Header);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionError(ex));
            }
        }
        /// <summary>
        /// Lấy file nhập khẩu mẫu
        /// </summary>
        /// <param name="key"></param>
        /// Created By: NQTruong (01/06/2023)
        /// <returns></returns>
        [HttpGet]
        [Route("SampleFile")]
        public IActionResult GetSampleFile(string key)
        {
            try
            {
                var name = new GetTableTitle().GetTableName(key);
                var service = GetService(key);
                if (service == null)
                {
                    return NotFound();
                }
                var fileData = service.GetSampleFile(name);
                if (fileData == null)
                {
                    return NotFound();
                }
                // Return the file
                HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
                return File(fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "example.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionError(ex));
            }
        }
        /// <summary>
        /// Xuất file excel
        /// </summary>
        /// <param name="model"></param>
        /// Created By: NQTruong (01/06/2023)
        /// <returns></returns>
        [HttpPost]
        [Route("ExportFile")]
        public async Task<IActionResult> ExportFile([FromBody] ExportModel model)
        {
            try
            {
                var name = new GetTableTitle().GetTableName(model.Key);
                var service = GetService(model.Key);
                if (service == null)
                {
                    return NotFound(new NotFoundError());
                }
                var fileData = await service.ExportFile(name, model);
                if (fileData == null)
                {
                    return NotFound(new NotFoundError());
                }
                // Return the file
                return File(fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{name}.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionError(ex));

            }
        }
        /// <summary>
        /// Lấy service dựa theo key
        /// </summary>
        /// <param name="key"></param>
        /// Created By: NQTruong (01/06/2023)
        /// <returns></returns>
        private dynamic? GetService(string key)
        {
            switch (key)
            {
                case "emulationtitle":
                    return _serviceProvider.GetService<IEmulationTitleImportService>();
                default:
                    return null;
            }
        } 
        #endregion
    }
}
