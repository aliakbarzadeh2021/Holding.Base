using System;
using System.Reflection;

namespace Holding.Base.Infrastructure.Utility
{
    /// <summary>
    /// کلاس کمکی برای کار با ریسورس ها
    /// </summary>
    public class ResourceHelper
    {
        public static string GetResourceLookup(Type resourceType, string property)
        {
            if ((resourceType != null) && (property != null))
            {
                PropertyInfo prop = resourceType.GetProperty(property, BindingFlags.Public | 
                    BindingFlags.Static);
                if (prop == null)
                {
                    throw new InvalidOperationException(string.Format("Resource Type Does Not Have Property"));
                }
                if (prop.PropertyType != typeof(string))
                {
                    throw new InvalidOperationException(string.Format("Resource Property is Not String Type"));
                }
                return (string)prop.GetValue(null, null);
            }
            return null;
        }
    }
}
