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
        /// Obtains all <see cref="FieldInfo"/> objects with the specified attribute <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to search for. This should derive from <see cref="Attribute"/>.</typeparam>
        /// <param name="obj">The object whose fields are being queried for the specified attribute.</param>
        /// <param name="includePrivate">A boolean value indicating whether to include private fields in the search.
        /// Defaults to false, which means only public and instance fields are considered by default.</param>
        /// <returns>An array of <see cref="FieldInfo"/> objects that are marked with the specified attribute <see cref="T"/>.
        /// The array includes private fields if <paramref name="includePrivate"/> is set to true.</returns>
        /// <remarks>
        /// Be cautious when setting <paramref name="includePrivate"/> to true, as accessing private fields can break
        /// encapsulation and lead to unintended side effects. It should be used only when necessary and with an understanding
        /// of the potential impact on class behavior.
        /// </remarks>
        public static FieldInfo[] GetFieldsWithAttribute<T>(this object obj, bool includePrivate = false) where T : Attribute
        {
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            if (includePrivate)
            {
                bindingFlags |= BindingFlags.NonPublic;
            }

            return obj.GetType()
                .GetFields(bindingFlags)
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
