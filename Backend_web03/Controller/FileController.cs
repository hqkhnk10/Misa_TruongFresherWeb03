using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Misa_TruongWeb03.BL.Service.FileService;
using System;

namespace FresherWeb03.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService, IWebHostEnvironment environment)
        {
            _fileService = fileService;
            _env = environment;
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
        public async Task<IActionResult> Download(string fileName)
        {
            try
            {
                var filePath = Path.Combine(_env.ContentRootPath, "FileStorage" , fileName);
                return File(System.IO.File.ReadAllBytes(filePath), "application/vnd.ms-excel", System.IO.Path.GetFileName(filePath));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}