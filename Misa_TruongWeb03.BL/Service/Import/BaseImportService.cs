using Aspose.Cells;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Misa_TruongWeb03.BL.Service.EmulationTitleService;
using Misa_TruongWeb03.BL.Service.FileServices;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.FileEntity;
using Misa_TruongWeb03.DL.Repository.FileRepository;
using System.Drawing;

namespace Misa_TruongWeb03.BL.Service.Import
{
    public class BaseImportService<T> : IBaseImportService<T>
    {
        private readonly IWebHostEnvironment _env;
        private readonly IFileRepository _fileRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IFileService _fileService;

        public BaseImportService(IWebHostEnvironment env, IFileRepository fileRepository, IMemoryCache memoryCache, IFileService fileService)
        {
            _env = env;
            _fileRepository = fileRepository;
            _memoryCache = memoryCache;
            _fileService = fileService;
        }
        public dynamic? GetSampleFile(string cacheKey)
        {
            if (_memoryCache.TryGetValue(cacheKey, out byte[] cachedData))
            {
                // Return the cached file if available
                return cachedData;
            }
            // Load the file from disk based on the file type
            var filePath = Path.Combine(_env.ContentRootPath, "FileStorage", $"{cacheKey}.xlsx");

            if (!System.IO.File.Exists(filePath))
            {
                return null;
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
            return fileData;
        }
        public async Task<FileValidateModel> Validate(IFormFile file, int sheetIndex, int header)
        {
            try
            {
                var res = await _fileService.Upload(file);
                var configEntity = await _fileRepository.MappingConfig(typeof(T).Name.ToLowerInvariant());
                var mapper = new ExcelConfigMapper();
                var config = mapper.MapToExcelConfig(configEntity);
                var data = await ReadExcel(res.FilePath, sheetIndex, header, config);
                data.FileName = res.FileStoreName;
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<byte[]> ExportFile(string fileName, ExportModel model)
        {
            try
            {
                //Gọi đến repo lấy config mapping
                var configEntity = await _fileRepository.MappingConfig(typeof(T).Name.ToLowerInvariant());
                var mapper = new ExcelConfigMapper();
                var configs = mapper.MapToExcelConfig(configEntity);
                //Gọi đến repo lấy danh sách
                var listData = await Get(model.Params);
                //Gọi hàm clone
                var sourceFile = "${fileName}_export";
                CloneExcelFile(fileName, sourceFile);
                //Viết vào file clone
                WriteToExcel(sourceFile, listData, configs);
                //Đọc dữ liệu của file
                return System.IO.File.ReadAllBytes(sourceFile);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Helper function
        public async Task<FileValidateModel> ReadExcel(string filePath, int sheetIndex, int header, List<ExcelMapping> configs)
        {
            Workbook workbook = new Workbook(filePath);
            Worksheet worksheet = workbook.Worksheets[sheetIndex];
            Cells cells = worksheet.Cells;

            int maxRow = worksheet.Cells.MaxDataRow + 1;
            int maxCol = worksheet.Cells.MaxDataColumn + 1;
            int Count = maxRow * maxCol;
            int errorMessageColumnIndex = maxCol;

            var validData = new List<dynamic>();
            var invalidData = new List<dynamic>();

            for (int row = header; row < maxRow; row++)
            {
                T instance = Activator.CreateInstance<T>();
                bool isValid = true;
                var errorMessage = "";
                foreach (var config in configs)
                {
                    int columnIndex = config.ColumnIndex;
                    string propertyName = config.PropertyName;
                    Type dataType = config.DataType;
                    object cellValue = cells[row, columnIndex].Value;
                    object formatValue = FormatCell(cellValue, config.FormatFunc);
                    //check trùng
                    if (config.IsDuplicateCheckEnabled && (IsDuplicateValue(formatValue, cells, columnIndex, row) || await IsDuplicateRecord(formatValue)))
                    {
                        isValid = false;
                        errorMessage += cells[0, columnIndex].Value + " Trùng dữ liệu.";
                    }
                    if (formatValue == null)
                    {
                        isValid = false;
                        errorMessage += cells[0, columnIndex].Value + " Không đúng định dạng";
                    }
                    if (!ValidateCell(formatValue, propertyName, config.ValidatorFunc))
                    {
                        isValid = false;
                        errorMessage += cells[0, columnIndex].Value + " Không hợp lệ.";
                    }
                    SetProperty(instance, propertyName, formatValue);
                }
                if (isValid)
                {
                    validData.Add(instance);
                }
                else
                {
                    cells[row, maxCol].PutValue(errorMessage);
                    // Apply red style to the error message cell
                    var style = cells[row, maxCol].GetStyle();
                    style.ForegroundColor = Color.Red;
                    style.Pattern = BackgroundType.Solid;
                    cells[row, maxCol].SetStyle(style);

                    invalidData.Add(instance);
                }
            }
            workbook.Save(filePath);
            return new FileValidateModel { ValidData = validData, InValidData = invalidData, Count = maxRow - header };
        }
        public void WriteToExcel(string newFilePath, List<dynamic> data, List<ExcelMapping> configs)
        {
            // Create a new workbook
            Workbook workbook = new Workbook(newFilePath);
            Worksheet worksheet = workbook.Worksheets[0]; // Get the first worksheet

            // Write data
            for (int rowIndex = 0; rowIndex < data.Count; rowIndex++)
            {
                dynamic item = data[rowIndex];
                for (int columnIndex = 0; columnIndex < configs.Count; columnIndex++)
                {
                    ExcelMapping config = configs[columnIndex];
                    object value = GetPropertyValue(item, config.PropertyName);
                    object formatValue = FormatCell(value, config.FormatFunc);
                    worksheet.Cells[rowIndex + 1, columnIndex].PutValue(formatValue);
                }
            }

            // Save the workbook
            workbook.Save(newFilePath);
        }

        // Helper method to retrieve property value using reflection
        private object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj);
        }
        public void CloneExcelFile(string sourceFilePath, string targetFilePath)
        {
            // Load the source workbook
            Workbook sourceWorkbook = new Workbook(sourceFilePath);

            // Create a new workbook and copy the source workbook
            Workbook targetWorkbook = new Workbook();
            targetWorkbook.Copy(sourceWorkbook);

            // Save the target workbook to the target file path
            targetWorkbook.Save(targetFilePath);
        }
        private bool ValidateCell(object cellValue, string propertyName, Func<object, bool> validatorFunc)
        {
            // Add your custom validation logic here
            if (cellValue == null)
            {
                if (validatorFunc == null)
                {
                    return true;
                }
                else
                {
                    return validatorFunc(cellValue);
                }
            }
            var propertyInfo = typeof(T).GetProperty(propertyName);
            Type valueType = cellValue.GetType();
            Type propertyType = propertyInfo.PropertyType;
            propertyType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

            bool typesMatch = valueType.Equals(propertyType);
            if (!typesMatch)
            {
                return false;
            }
            if (validatorFunc == null)
                return true;

            return validatorFunc(cellValue);
        }
        private dynamic FormatCell(object cellValue, Func<object, dynamic> formatFunc)
        {
            if (formatFunc == null)
                return cellValue;

            return formatFunc(cellValue);
        }
        private void SetProperty(T instance, string propertyName, object value)
        {
            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                Type propertyType = propertyInfo.PropertyType;
                object convertedValue = null;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // Property type is nullable, get the underlying type
                    propertyType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                }
                try
                {
                    convertedValue = Convert.ChangeType(value, propertyType);
                }
                catch (InvalidCastException)
                {
                    // Handle the case where the value cannot be converted to the desired type
                    // Set a default value or null based on the property type
                    if (propertyType.IsValueType)
                    {
                        // Value type, set default value
                        convertedValue = Activator.CreateInstance(propertyType);
                    }
                    else
                    {
                        // Reference type, set null
                        convertedValue = null;
                    }
                }

                propertyInfo.SetValue(instance, convertedValue);
            }
        }
        private bool IsDuplicateValue(object value, Cells cells, int columnIndex, int currentRow)
        {
            // Iterate through the rows above the current row
            for (int row = 0; row < currentRow; row++)
            {
                object cellValue = cells[row, columnIndex].Value;

                // Compare cell values for duplicates
                if (value != null && cellValue != null && value.Equals(cellValue))
                {
                    return true; // Duplicate value found
                }
            }

            return false; // No duplicate value found
        }
        public virtual async Task<bool> IsDuplicateRecord(object cellValue)
        {
            return true;
        }
        public virtual async Task<dynamic> Get(dynamic parameters)
        {
            return null;
        }
    }
}
