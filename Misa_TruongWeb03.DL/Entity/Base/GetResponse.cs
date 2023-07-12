using Misa_TruongWeb03.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Entity.Base
{
    public class GetResponse
    {
        public object Data { get; set; }
        public Pagination Pagination { get; set; }
    }
    public class Pagination
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public int TotalPage { get
            {
                if (Count > 0)
                {
                    return (int)Math.Ceiling((double)Count / PageSize);
                }
                else return 0;
            }
        }
    }
}
