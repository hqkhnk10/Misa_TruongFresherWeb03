using Aspose.Cells;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Misa_TruongWeb03.BL.Service.FileServices;
using Misa_TruongWeb03.Common.DTO;
using Misa_TruongWeb03.Common.Entity.FileEntity;
using Misa_TruongWeb03.DL.Repository.FileRepository;
using System.Drawing;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.BL.Service.BaseImport
{
    public class BaseImportService<T> : IBaseImportService<T>
    {
        #region Property
        private readonly IWebHostEnvironment _env;
        private readonly IFileRepository _fileRepository;
        private readonly IFileService _fileService;
        #endregion
        #region Constructor
        public BaseImportService(IWebHostEnvironment env, IFileRepository fileRepository, IFileService fileService)
        {
            _env = env;
            _fileRepository = fileRepository;
            _fileService = fileService;
        }
        #endregion
        #region Method

        /// <summary>
        /// Xác thực dữ liệu của file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        /// Created By: NQTruong (01/06/2023)
        public virtual async Task<FileValidateModel> Validate(IFormFile file, int sheetIndex, int header)
        {
            try
            {
                //upload file
                var res = await _fileService.Upload(file);
                
                //get config
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
        /// <summary>
        /// Đọc file Excel và trả ra lỗi nếu file ko hợp lệ
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="header"></param>
        /// <param name="configs"></param>
        /// <returns></returns>
        /// Created By: NQTruong (01/06/2023)
        public async Task<FileValidateModel> ReadExcel(string filePath, int sheetIndex, int header, List<ExcelMapping> configs)
        {
            
            using var workbook = new Workbook(filePath);
            Worksheet worksheet = workbook.Worksheets[sheetIndex];
            Cells cells = worksheet.Cells;

            int maxRow = worksheet.Cells.MaxDataRow + 1;
            int maxCol = worksheet.Cells.MaxDataColumn + 1;
            int Count = maxRow * maxCol;
            int errorMessageColumnIndex = maxCol;

            var validData = new List<dynamic>();
            var invalidData = new List<dynamic>();

            var invalidFileName = $"{Path.GetFileNameWithoutExtension(filePath)}_invalid";
            var validIndex = new List<int>();
            var findConfigs = new List<ExcelMapping>();
            var lastIndex = -1;

            foreach (var config in configs)
            {
                for (int i = lastIndex + 1; i < maxRow; i++)
                {
                    var mapper = new ExcelConfigMapper();
                    var cloneConfig = mapper.MapExcelConfig(config);
                    if (cells[header, i].Value == null)
                    {
                        continue;
                    }
                    if(MapColumn(cells[header, i].Value, i, config)){
                        lastIndex=i;
                        cloneConfig.ColumnIndex = lastIndex;
                        findConfigs.Add(cloneConfig);
                    }
                }
            }


            for (int row = header + 1; row < maxRow; row++)
            {
                var instance = Activator.CreateInstance<T>();
                bool isValid = true;
                var errorMessage = "";
                foreach (var config in findConfigs)
                {
                    int columnIndex = config.ColumnIndex;
                    string propertyName = config.PropertyName;
                    Type dataType = config.DataType;
                    var cellValue = cells[row, columnIndex].Value;
                    //check không được để trống
                    if (config.IsRequired && (cellValue == null || string.IsNullOrEmpty(cellValue.ToString())))
                    {
                        isValid = false;
                        errorMessage += cells[header - 1, columnIndex].Value + " Không được để trống.";
                    }
                    object formatValue = FormatCell(cellValue, config.FormatFunc);
                    //check trùng
                    if (config.IsDuplicateCheckEnabled && (IsDuplicateValue(formatValue, cells, columnIndex, row) || await IsDuplicateRecord(formatValue)))
                    {
                        isValid = false;
                        errorMessage += cells[header - 1, columnIndex].Value + " Trùng dữ liệu.";
                    }
                    ////check format dữ liệu
                    //if (formatValue == null && cellValue != null)
                    //{
                    //    isValid = false;
                    //    errorMessage += cells[header - 1, columnIndex].Value + " Không đúng định dạng";
                    //}
                    //check theo hàm
                    if (config.ValidatorFunc != null && !ValidateCell(formatValue, propertyName, config.ValidatorFunc))
                    {
                        isValid = false;
                        errorMessage += cells[header - 1, columnIndex].Value + " Không hợp lệ.";
                    }
                    SetProperty(instance, propertyName, formatValue);
                }
                if (isValid)
                {
                    validData.Add(instance);
                    validIndex.Add(row);
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
            RemoveRowsFromExcel(filePath, invalidFileName, validIndex);
            validData = FormatReturnData(validData);

            return new FileValidateModel { ValidData = validData, InValidData = invalidData, Count = maxRow - header, InvalidFilePath = $"{invalidFileName}.xlsx" };
        }
        /// <summary>
        /// Viết dữ liệu vào file để xuất ra
        /// </summary>
        /// <param name="newFilePath"></param>
        /// <param name="data"></param>
        /// <param name="configs"></param>
        /// Created By: NQTruong (01/06/2023)
        public void WriteToExcel(string newFilePath, dynamic data, List<ExcelMapping> configs)
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
        /// Tạo link file excel mới dựa trên file cũ
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="targetFilePath"></param>
        /// Created By: NQTruong (01/06/2023)
        public void CloneExcelFile(string sourceFilePath, string targetFilePath)
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
        /// Copy sang file excel khác
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationFilePath"></param>
        /// <param name="sheetName"></param>
        /// <param name="rowIndices"></param>
        public void RemoveRowsFromExcel(string sourceFilePath, string destinationFilePath, List<int> rowIndices)
        {
            // Add a new worksheet to the destination workbook
            using var destinationWorkbook = new Workbook(sourceFilePath);
            var destinationWorksheet = destinationWorkbook.Worksheets[0];

            // Copy rows from source to destination worksheet based on the specified row indices
            foreach (int rowIndex in rowIndices)
            {
                // Get the source row
                destinationWorksheet.Cells.DeleteRow(rowIndex);
            }
            var filePath = Path.Combine(_env.ContentRootPath, "FileStorage", $"{destinationFilePath}.xlsx");
            // Save the destination workbook to a new Excel file
            destinationWorkbook.Save(filePath, SaveFormat.Xlsx);
        }
        /// <summary>
        /// Gọi đến hàm validate của từng cell theo config
        /// </summary>
        /// <param name="cellValue"></param>
        /// <param name="propertyName"></param>
        /// <param name="validatorFunc"></param>
        /// <returns></returns>
        /// Created By: NQTruong (01/06/2023)
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
                    return validatorFunc(cellValue.ToString());
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
        /// Gán dữ liệu cho object không biết trước property
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// Created By: NQTruong (01/06/2023)
        public virtual void SetProperty(T instance, string propertyName, object value)
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
        /// <summary>
        /// Kiểm tra trùng trong file
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cells"></param>
        /// <param name="columnIndex"></param>
        /// <param name="currentRow"></param>
        /// <returns></returns>
        /// Created By: NQTruong (01/06/2023)
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
        /// <summary>
        /// Kiểm tra trùng trong DB
        /// </summary>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        public virtual async Task<bool> IsDuplicateRecord(object cellValue)
        {
            return true;
        }
        public virtual bool MapColumn(object columnName,int index, ExcelMapping config)
        {
            if (columnName.ToString().Contains(config.ColumnName))
            {
                return true;
            }
            return false;
        }
        public virtual dynamic FormatReturnData(List<dynamic>? data)
        {
            return data;
        }
        #endregion
    }
}
