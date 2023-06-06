using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Helper
{
    public class GetTableTitle
    {
        /// <summary>
        /// Lấy tên table dựa theo keys
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetTableName(string key)
        {
            // Define the mapping or lookup mechanism
            Dictionary<string, string> classToTableMapping = new Dictionary<string, string>
    {
        { "emulationtitle", "Danh hiệu thi đua" },
        // Add more mappings as needed
    };

            // Check if the class name exists in the mapping
            if (classToTableMapping.ContainsKey(key))
            {
                return classToTableMapping[key];
            }

            // Return the original class name if no mapping is found
            return key;
        }
    }
}
