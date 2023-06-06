using Misa_TruongWeb03.Common.Format;
using Misa_TruongWeb03.Common.Validator;
using System.Reflection;

namespace Misa_TruongWeb03.Common.Entity.FileEntity
{
    public class ExcelMapping
    {
        public int Id { get; set; }
        public int ColumnIndex { get; set; }
        public string? PropertyName { get; set; }
        public Type? DataType { get; set; }
        public Func<object, bool>? ValidatorFunc { get; set; }
        public Func<object, dynamic>? FormatFunc { get; set; }
        public string? TableKey { get; set; }
        public bool IsDuplicateCheckEnabled { get; set; }
    }
    public class ExcelConfigEntity
    {
        public int Id { get; set; }
        public int ColumnIndex { get; set; }
        public string? PropertyName { get; set; }
        public string DataType { get; set; }
        public string ValidatorFunc { get; set; }
        public string FormatFunc { get; set; }
        public string? TableKey { get; set; }
        public bool IsDuplicateCheckEnabled { get; set; }
    }
    public class ExcelConfigMapper
    {
        public List<ExcelMapping> MapToExcelConfig(List<ExcelConfigEntity> entity)
        {
            var config = new List<ExcelMapping>();
            foreach (var item in entity)
            {
                var excelEntity = new ExcelMapping
                {
                    ColumnIndex = item.ColumnIndex,
                    PropertyName = item.PropertyName,
                    TableKey = item.TableKey,
                    IsDuplicateCheckEnabled = item.IsDuplicateCheckEnabled,
                    DataType = Type.GetType(item.DataType) ?? typeof(string),
                    ValidatorFunc = ConvertToValidatorFunc(item.ValidatorFunc),
                    FormatFunc = ConvertToFormatFunc(item.FormatFunc)
                };
                config.Add(excelEntity);
            }
            return config;
        }

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
