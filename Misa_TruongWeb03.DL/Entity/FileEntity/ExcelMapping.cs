using Misa_TruongWeb03.Common.Format;
using Misa_TruongWeb03.Common.Validator;
using System.Reflection;

namespace Misa_TruongWeb03.Common.Entity.FileEntity
{
    public class ExcelMapping
    {
        public int Id { get; set; }
        public int ColumnIndex { get; set; }
        public string ColumnName { get; set; }
        public string? PropertyName { get; set; }
        public Type? DataType { get; set; }
        public Func<object, bool>? ValidatorFunc { get; set; }
        public Func<object, dynamic>? FormatFunc { get; set; }
        public Func<object, dynamic>? ConvertFunc { get; set; }
        public string? TableKey { get; set; }
        public bool IsDuplicateCheckEnabled { get; set; }
        public bool IsRequired { get; set; }
    }
    public class ExcelConfigEntity
    {
        public int Id { get; set; }
        public int ColumnIndex { get; set; }
        public string ColumnName { get; set; }
        public string? PropertyName { get; set; }
        public string DataType { get; set; }
        public string ValidatorFunc { get; set; }
        public string FormatFunc { get; set; }
        public string ConvertFunc { get; set; }
        public string? TableKey { get; set; }
        public bool IsDuplicateCheckEnabled { get; set; }
        public bool IsRequired { get; set; }
    }
    public class ExcelConfigMapper
    {
        /// <summary>
        /// Map dữ liệu config từ DB sang ExcelConfig
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<ExcelMapping> MapToExcelConfig(List<ExcelConfigEntity> entity)
        {
            var config = new List<ExcelMapping>();
            foreach (var item in entity)
            {
                var excelEntity = new ExcelMapping
                {
                    ColumnIndex = item.ColumnIndex,
                    PropertyName = item.PropertyName,
                    ColumnName = item.ColumnName,
                    TableKey = item.TableKey,
                    IsDuplicateCheckEnabled = item.IsDuplicateCheckEnabled,
                    IsRequired = item.IsRequired,
                    DataType = Type.GetType(item.DataType) ?? typeof(string),
                    ValidatorFunc = ConvertToValidatorFunc(item.ValidatorFunc),
                    FormatFunc = ConvertToFormatFunc(item.FormatFunc),
                    ConvertFunc = ConvertToFormatFunc(item.ConvertFunc),
                };
                config.Add(excelEntity);
            }
            return config;
        }
        public ExcelMapping MapExcelConfig(ExcelMapping item)
        {
            var excelEntity = new ExcelMapping
            {
                ColumnIndex = item.ColumnIndex,
                PropertyName = item.PropertyName,
                ColumnName = item.ColumnName,
                TableKey = item.TableKey,
                IsDuplicateCheckEnabled = item.IsDuplicateCheckEnabled,
                IsRequired = item.IsRequired,
                DataType = item.DataType,
                ValidatorFunc = item.ValidatorFunc,
                FormatFunc = item.FormatFunc,
                ConvertFunc = item.ConvertFunc,
            };
            return excelEntity;
        }
        /// <summary>
        /// Lấy hàm validate theo tên lưu trong DB
        /// </summary>
        /// <param name="funcName"></param>
        /// <returns></returns>
        private Func<object, bool> ConvertToValidatorFunc(string funcName)
        {
            if (string.IsNullOrEmpty(funcName))
                return null;

            Func<object, bool> func = obj =>
            {
                MethodInfo? methodInfo = typeof(ValidatorMethod).GetMethod(funcName);
                if (methodInfo == null)
                {
                    return true;
                }
                return (bool)methodInfo.Invoke(null, new[] { obj });
            };

            return func;
        }
        /// <summary>
        /// Lấy hàm format theo tên lưu trong DB
        /// </summary>
        /// <param name="funcName"></param>
        /// <returns></returns>
        private Func<object, object>? ConvertToFormatFunc(string funcName)
        {
            if (string.IsNullOrEmpty(funcName))
                return null;

            Func<object, object> func = obj =>
            {
                MethodInfo? methodInfo = typeof(FormatFunction).GetMethod(funcName);
                if (methodInfo == null)
                {
                    return true;
                }
                return methodInfo.Invoke(null, new[] { obj });
            };
            return func;
        }
    }
}
