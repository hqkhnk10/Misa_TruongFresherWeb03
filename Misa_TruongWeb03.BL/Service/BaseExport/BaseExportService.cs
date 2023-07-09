using Aspose.Cells;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Misa_TruongWeb03.Common.Entity.FileEntity;
using Misa_TruongWeb03.DL.Repository.FileRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.BL.Service.BaseExport
{
    public class BaseExportService<T> : IBaseExportService<T>
    {
        private readonly IWebHostEnvironment _env;
        private readonly IFileRepository _fileRepository;
        private readonly IMemoryCache _memoryCache;

        public BaseExportService(IWebHostEnvironment env, IFileRepository fileRepository, IMemoryCache memoryCache)
        {
            _env = env;
            _fileRepository = fileRepository;
            _memoryCache = memoryCache;
        }
        /// <summary>
        /// Lấy file mẫu theo key
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        /// Created By: NQTruong (01/06/2023)
        public byte[]? GetSampleFile(string cacheKey)
        {
            if (_memoryCache.TryGetValue(cacheKey, out byte[] cachedData))
            {
                // Return the cached file if available
                return cachedData;
            }
            // Load the file from disk based on the file type
            var filePath = Path.Combine(_env.ContentRootPath, "FileStorage", $"{cacheKey}.xlsx");

            if (!File.Exists(filePath))
            {
                return null;
            }

            var fileData = File.ReadAllBytes(filePath);

            // Cache the file data
            var options = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow =
                                    TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1200)
            };
            _memoryCache.Set(cacheKey, fileData, options);
            return fileData;
        }
        public async Task<byte[]> ExportFile(string fileName, ExportModel model)
        {
            try
            {
                //Gọi đến repo lấy danh sách
                var listData = await Get(model.Parameters);

                //Gọi đến repo lấy config mapping
                var configEntity = await _fileRepository.MappingConfig(typeof(T).Name.ToLowerInvariant());
                var mapper = new ExcelConfigMapper();
                var configs = mapper.MapToExcelConfig(configEntity);

                //Gọi hàm clone
                var sourceFile = Path.Combine(_env.ContentRootPath, "FileStorage", $"{fileName}_export.xlsx");
                var filePath = Path.Combine(_env.ContentRootPath, "FileStorage", $"{fileName}.xlsx");
                var sourceFilePath = Path.Combine(_env.ContentRootPath, "FileStorage", $"{sourceFile}.xlsx");
                CloneExcelFile(filePath, $"{fileName}_export");
                //Viết vào file clone
                WriteToExcel(sourceFile, listData, configs);
                //Đọc dữ liệu của file
                return File.ReadAllBytes(sourceFile);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Tạo link file excel mới dựa trên file cũ
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="targetFilePath"></param>
        /// Created By: NQTruong (01/06/2023)
        private void CloneExcelFile(string sourceFilePath, string targetFilePath)
        {
            //// Load the source workbook
            //using var sourceWorkbook = new Workbook(sourceFilePath);
            //// Create a new workbook and copy the source workbook
            using var targetWorkbook = new Workbook(sourceFilePath);
            // Save the target workbook to the target file path
            var filePath = Path.Combine(_env.ContentRootPath, "FileStorage", $"{targetFilePath}.xlsx");
            targetWorkbook.Save(filePath);
        }
        /// <summary>
        /// Viết dữ liệu vào file để xuất ra
        /// </summary>
        /// <param name="newFilePath"></param>
        /// <param name="data"></param>
        /// <param name="configs"></param>
        /// Created By: NQTruong (01/06/2023)
        private void WriteToExcel(string newFilePath, dynamic data, List<ExcelMapping> configs)
        {
            // Create a new workbook
            using var workbook = new Workbook(newFilePath);
            var worksheet = workbook.Worksheets[0]; // Get the first worksheet

            // Write data
            for (int rowIndex = 0; rowIndex < data.Count; rowIndex++)
            {
                dynamic item = data[rowIndex];
                for (int columnIndex = 0; columnIndex < configs.Count; columnIndex++)
                {
                    ExcelMapping config = configs[columnIndex];
                    object value = GetPropertyValue(item, config.PropertyName);
                    object formatValue = FormatCell(value, config.ConvertFunc);
                    worksheet.Cells[rowIndex + 2, columnIndex].PutValue(formatValue);
                }
            }

            // Save the workbook
            workbook.Save(newFilePath);
        }
        /// <summary>
        /// Lấy dữ liệu của object dùng reflection
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        /// Created By: NQTruong (01/06/2023)
        private object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj);
        }
        /// <summary>
        /// Gọi đến hàm format của từng cell theo config
        /// </summary>
        /// <param name="cellValue"></param>
        /// <param name="formatFunc"></param>
        /// <returns></returns>
        /// Created By: NQTruong (01/06/2023)
        private dynamic FormatCell(object cellValue, Func<object, dynamic> formatFunc)
        {
            if (formatFunc == null)
                return cellValue;

            return formatFunc(cellValue);
        }
        /// <summary>
        /// Lấy dữ liệu của bảng để viết vào excel
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// Created By: NQTruong (01/06/2023)
        public virtual async Task<dynamic> Get(dynamic parameters)
        {
            return null;
        }
    }
}
