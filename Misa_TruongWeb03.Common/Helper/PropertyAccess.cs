namespace Misa_TruongWeb03.Common.Helper;

using System;
using System.Reflection;

public static class PropertyAccess
{
    /// <summary>
    /// Get value of property in object
    /// </summary>
    /// <param name="obj">Object model</param>
    /// <param name="propertyName">property</param>
    /// <returns>value</returns>
    /// <exception cref="ArgumentException"></exception>
    /// Created By: QTNgo (23/05/2023)
    public static dynamic? GetPropertyValue(object obj, string propertyName)
    {
        Type objectType = obj.GetType();
        var property = objectType.GetProperty(propertyName);

        if (property != null)
        {
            return property.GetValue(obj);
        }
        else
        {
            return null;
        }

        throw new ArgumentException($"Property '{propertyName}' not found on object of type '{objectType.Name}'.");
    }
}
