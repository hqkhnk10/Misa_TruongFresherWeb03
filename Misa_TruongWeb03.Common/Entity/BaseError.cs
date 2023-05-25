using Misa_TruongWeb03.Common.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Entity
{
    public class DatabaseError : BaseEntity
    {
        public DatabaseError()
        {
            ErrorCode = 500;
            DevMsg = VN.NoAffectedRows;
            UserMsg = VN.Error500;
        }
    }
}
