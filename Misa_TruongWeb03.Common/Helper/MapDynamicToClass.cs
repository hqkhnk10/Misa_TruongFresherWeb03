using System;
using System.Dynamic;
using System.Reflection;
using System.Text.Json;

namespace Misa_TruongWeb03.Common.Helper;
public class CustomMap
{
    /// <summary>
    /// Map dynamic to Object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dynamicObj"></param>
    /// <returns></returns>
    public T MapDynamicToObject<T>(dynamic dynamicObj)
    {
        T obj = Activator.CreateInstance<T>();

        foreach (PropertyInfo propInfo in typeof(T).GetProperties())
        {
            if (propInfo.CanWrite)
            {
                string propName = propInfo.Name;
                dynamic propValue = null;

                try
                {
                    propValue = dynamicObj[propName];
                }
                catch (Exception)
                {
                    // Property not found in the dynamic object
                    continue;
                }

                try
                {
                    object convertedValue = Convert.ChangeType(propValue, propInfo.PropertyType);
                    propInfo.SetValue(obj, convertedValue);
                }
                catch (Exception)
                {
                    // Unable to convert the value to the property type
                    continue;
                }
            }
        }

        return obj;
    }
}



