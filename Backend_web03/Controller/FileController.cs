using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic.FileIO;
using Misa_TruongWeb03.BL.Service.FileService;
using Misa_TruongWeb03.Common.Entity;
using System;

namespace FresherWeb03.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IFileService _fileService;
        private readonly IMemoryCache _memoryCache;
        public FilesController(IFileService fileService, IWebHostEnvironment environment, IMemoryCache memoryCache)
        {
            _fileService = fileService;
            _env = environment;
            _memoryCache = memoryCache;
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
                var filePath = Path.Combine(_env.ContentRootPath, "FileStorage", fileName);
                return File(System.IO.File.ReadAllBytes(filePath), "application/vnd.ms-excel", System.IO.Path.GetFileName(filePath));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{cacheKey}")]
        public IActionResult Get(string cacheKey)
        {
            if (_memoryCache.TryGetValue(cacheKey, out byte[] cachedData))
            {
                // Return the cached file if available
                return File(cachedData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{cacheKey}.xlsx");
            }
            // Load the file from disk based on the file type
            var filePath = Path.Combine(_env.ContentRootPath, "FileStorage", $"{cacheKey}.xlsx");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found." + filePath);
            }

            var fileData = System.IO.File.ReadAllBytes(filePath);

            // Cache the file data
            var options = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow =
                                    TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1200)
            };
            _memoryCache.Set(cacheKey, fileData, options);
            // Return the file
            return File(fileData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{cacheKey}.xlsx");


        }
    }
}