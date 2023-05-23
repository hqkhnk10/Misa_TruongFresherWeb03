using System;
using System.Linq.Expressions;
using System.Reflection;
namespace Misa_TruongWeb03.Common.Helper;

public static class ParameterObjectBuilder
{
    /// <summary>
    /// Create a parameter object where each property is assigned the value of the corresponding property in the model
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    /// <param name="model"></param>
    /// <returns>Object</returns>
    /// Created By: QTNgo (22/05/2023)
    public static object? CreateParameterObject<T>(T model)
    {
        var parameterObject = Activator.CreateInstance<T>();
        var modelProperties = typeof(T).GetProperties();

        foreach (var property in modelProperties)
        {
            var propertyValue = property.GetValue(model);
            var parameterProperty = typeof(T).GetProperty(property.Name);

            if (parameterProperty != null)
            {
                var parameterPropertyValue = Expression.Constant(propertyValue, property.PropertyType);
                var parameterPropertySetExpression = Expression.Assign(Expression.Property(Expression.Constant(parameterObject), parameterProperty), parameterPropertyValue);
                var parameterPropertySetLambda = Expression.Lambda<Action>(parameterPropertySetExpression);
                parameterPropertySetLambda.Compile().Invoke();
            }
        }

        return parameterObject;
    }
}
