using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

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
        /// <param name="applyOmitLogic">If true, omit fields, otherwise find all fields.</param>
        /// <returns>An array of <see cref="FieldInfo"/> objects that are marked with the specified attribute <see cref="T"/>.
        /// The array includes private fields if <paramref name="includePrivate"/> is set to true.</returns>
        /// <remarks>
        /// Be cautious when setting <paramref name="includePrivate"/> to true, as accessing private fields can break
        /// encapsulation and lead to unintended side effects. It should be used only when necessary and with an understanding
        /// of the potential impact on class behavior.
        /// </remarks>
        public static FieldInfo[] GetFieldsWithAttribute<T>(
            this object obj,
            bool includePrivate = false,
            bool applyOmitLogic = true) where T : PropertyAttribute
        {
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            if (includePrivate)
            {
                bindingFlags |= BindingFlags.NonPublic;
            }

            Type type = obj.GetType();

            return type.GetFields(bindingFlags)
                .Where(field =>
                {
                    T attr = field.GetCustomAttribute<T>();
                    if (attr == null)
                    {
                        return false;
                    }

                    if (applyOmitLogic)
                    {
                        OmittableAttribute omitAttr = attr as OmittableAttribute;
                        if (omitAttr?.OmitFieldName != null)
                        {
                            FieldInfo omitField = type.GetField(omitAttr.OmitFieldName, bindingFlags);
                            if (omitField != null && omitField.FieldType == typeof(bool))
                            {
                                return (bool)omitField.GetValue(obj);
                            }
                        }
                    }

                    return true;
                })
                .ToArray();
        }

        /// <summary>
        /// Maps marked variables into a <see cref="Func{T}>"/> array.
        /// </summary>
        /// <typeparam name="T"><see cref="Attribute"/> to query.</typeparam>
        /// <param name="obj">The object containing marked variables.</param>
        /// <param name="variableFunction">Output <see cref="Func{T}"/> array.</param>
        /// <param name="updateFunction">Function to run before returning variables.</param>
        public static void InitializeVariables<T>(
            this object obj,
            out Func<float[]> variableFunction,
            Action updateFunction = null) where T : PropertyAttribute
        {
            FieldInfo[] fields = obj.GetFieldsWithAttribute<T>(includePrivate: true);

            variableFunction = () => fields.SelectMany(f =>
            {
                updateFunction?.Invoke();

                if (f.FieldType == typeof(Vector3))
                {
                    Vector3 vec = (Vector3)f.GetValue(obj);
                    return new float[] { vec.x, vec.z, vec.y };
                }

                if (f.FieldType == typeof(Quaternion))
                {
                    Quaternion quat = (Quaternion)f.GetValue(obj);
                    return new float[] { quat.w, quat.x, quat.z, quat.y };
                }

                if (f.FieldType == typeof(float[]))
                {
                    return (float[])f.GetValue(obj);
                }

                return new float[] { Convert.ToSingle(f.GetValue(obj)) };
            })
                .ToArray();
        }
    }
}
