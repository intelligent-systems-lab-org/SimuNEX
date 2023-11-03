using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Marks the variable as a parameter to the system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class ParameterAttribute : PropertyAttribute
    {
    }

    /// <summary>
    /// Marks the variable as an input to the system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class InputAttribute : PropertyAttribute
    {
    }

    /// <summary>
    /// Marks the variable so that <see cref="Fault"/> objects can be applied.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class Faultable : Attribute
    {
    }

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
