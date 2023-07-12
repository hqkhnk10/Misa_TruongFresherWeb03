using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.FileServices;
using Misa_TruongWeb03.Common.Entity.Base;
using Misa_TruongWeb03.Common.Entity.FileEntity;
using Misa_TruongWeb03.Common.Helper;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.BL.Service.EmisStudy.QuestionService;

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

            var res = await _fileService.Upload(file);
            return Ok(res);

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

            var fileData = _fileService.Download(fileName);
            return File(fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

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
            var service = GetImportService(model.Key);


            var res = await service.Validate(model.File, model.SheetIndex, model.Header);
            return Ok(res);

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

            var name = new GetTableTitle().GetTableName(key);
            var service = GetExportService(key);
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

            var name = new GetTableTitle().GetTableName(model.Key);
            var service = GetImportService(model.Key);

            var fileData = await service.ExportFile(name, model);

            // Return the file
            return File(fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{name}.xlsx");

        }
        /// <summary>
        /// Lấy import service dựa theo key
        /// </summary>
        /// <param name="key"></param>
        /// Created By: NQTruong (01/06/2023)
        /// <returns></returns>
        private dynamic? GetImportService(string key)
        {
            switch (key)
            {

                case "emisquestion":
                    return _serviceProvider.GetService<IQuestionImportService>();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Lấy export service dựa theo key
        /// </summary>
        /// <param name="key"></param>
        /// Created By: NQTruong (01/06/2023)
        /// <returns></returns>
        private dynamic? GetExportService(string key)
        {
            switch (key)
            {
                case "emisquestion":
                    return _serviceProvider.GetService<IQuestionExportService>();
                default:
                    return null;
            }
        }
        #endregion
    }
}
