using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Validator
{
    public class ValidatorMethod
    {
        public static bool Required(string? value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
