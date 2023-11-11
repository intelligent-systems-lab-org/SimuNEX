using System;
using System.Linq;
using System.Reflection;

namespace SimuNEX
{
    /// <summary>
    /// Extension methods for SimuNEX objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Obtains all <see cref="FieldInfo"/> objects with attribute <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="Attribute"/> type.</typeparam>
        /// <param name="obj">Object containing queried attributes.</param>
        /// <returns>Array of <see cref="FieldInfo"/> objects queried with attribute <see cref="T"/>.</returns>
        public static FieldInfo[] GetFieldsWithAttribute<T>(this object obj) where T : Attribute
        {
            return obj.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(f => Attribute.IsDefined(f, typeof(T)))
                .ToArray();
        }

        /// <summary>
        /// Obtains all <see cref="PropertyInfo"/> objects with attribute <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="Attribute"/> type.</typeparam>
        /// <param name="obj">Object containing queried attributes.</param>
        /// <returns>Array of <see cref="PropertyInfo"/> objects queried with attribute <see cref="T"/>.</returns>
        public static PropertyInfo[] GetPropertiesWithAttribute<T>(this object obj) where T : Attribute
        {
            return obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => Attribute.IsDefined(p, typeof(T)))
                .ToArray();
        }

        /// <summary>
        /// Maps marked variables into a <see cref="Func{T}>"/> array.
        /// </summary>
        /// <typeparam name="T"><see cref="Attribute"/> to query.</typeparam>
        /// <param name="obj">The object containing marked variables.</param>
        /// <param name="variableFunction">Output <see cref="Func{T}"/> array.</param>
        public static void InitializeVariables<T>(this object obj, out Func<float[]> variableFunction) where T : Attribute
        {
            FieldInfo[] fields = obj.GetFieldsWithAttribute<T>();
            variableFunction = () => fields.Select(f => Convert.ToSingle(f.GetValue(obj))).ToArray();
        }
    }
}
