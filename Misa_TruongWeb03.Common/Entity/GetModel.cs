using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Entity
{
    public class GetModel
    {
        public int pageSize { get; set; } = 10;
        public int pageIndex { get; set; } = 1;
        public string? keyword { get; set; } = null;

        // Add other properties as needed
    }
}
