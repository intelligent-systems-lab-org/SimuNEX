using System;
using System.Linq;
using System.Reflection;

namespace SimuNEX
{
    public static class ObjectExtensions
    {
        public static FieldInfo[] GetFieldsWithAttribute<T>(this object obj) where T : Attribute
        {
            return obj.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(f => Attribute.IsDefined(f, typeof(T)))
                .ToArray();
        }

        public static PropertyInfo[] GetPropertiesWithAttribute<T>(this object obj) where T : Attribute
        {
            return obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => Attribute.IsDefined(p, typeof(T)))
                .ToArray();
        }
    }
}
