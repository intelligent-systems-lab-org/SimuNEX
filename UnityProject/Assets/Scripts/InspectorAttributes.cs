using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SimuNEX
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class ParameterAttribute : PropertyAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class InputAttribute : PropertyAttribute
    {
    }

    public static class ObjectExtensions
    {
        public static int CountFieldsWithAttribute<T>(this object obj) where T : Attribute
        {
            return obj.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Count(f => Attribute.IsDefined(f, typeof(T)));
        }
    }
}
