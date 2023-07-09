using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Helper
{
    public class DynamicParametersAdd
    {
        public static DynamicParameters CreateParameterDynamic<T>(T model)
        {
            var dynamicParams = new DynamicParameters();
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                dynamicParams.Add(prop.Name, prop.GetValue(model));
            }
            return dynamicParams;
        }
    }
}
