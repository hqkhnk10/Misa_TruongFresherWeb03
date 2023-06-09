﻿using Microsoft.AspNetCore.Mvc;
using Misa_TruongWeb03.BL.Service.FileServices;
using Misa_TruongWeb03.BL.Service.Import;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Helper;

namespace Misa_TruongWeb03_File.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController<T> : ControllerBase
    {
        private IServiceProvider _serviceProvider;
        private IFileService<T> _fileService;
        public FileController(IServiceProvider serviceProvider, IFileService<T> fileService)
        {
            _serviceProvider = serviceProvider;
            _fileService = fileService;
        }

        /// <summary>
        /// Single File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("PostSingleFile")]
        public async Task<IActionResult> PostSingleFile(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest();
            }

            try
            {
                var res = await _fileService.Upload(file);
                return Ok(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("download")]
        public IActionResult Download(string fileName)
        {
            try
            {
                var fileData = _fileService.Download(fileName);
                return File(fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("ValidateFile")]
        public async Task<IActionResult> ValidateFile([FromForm] ValidateFileDTO model, string key)
        {
            var service = GetService(key);
            if (service == null)
            {
                return NotFound();
            }
            try
            {
                var res = await service.Validate(model.File, model.SheetIndex, model.Header);
                return Ok(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("SampleFile")]
        public IActionResult GetSampleFile(string key)
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
    }
}
