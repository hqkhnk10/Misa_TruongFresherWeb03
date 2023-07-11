using Misa_TruongWeb03.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.DL.Entity.Base
{
    public class BaseGet<T>
    {
        public T Data { get; set; }
        public Pagination Pagination { get; set; }
    }
}
