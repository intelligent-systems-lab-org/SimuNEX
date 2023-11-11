using System;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Marks the variable as a parameter to the system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ParameterAttribute : PropertyAttribute
    {
    }

    /// <summary>
    /// Marks the variable as an input to the system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class InputAttribute : PropertyAttribute
    {
    }

    /// <summary>
    /// Marks the property so that <see cref="Fault"/> objects can be applied.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class FaultableAttribute : PropertyAttribute
    {
        /// <summary>
        /// List of supported faults.
        /// </summary>
        public Type[] SupportedFaults { get; }

        /// <summary>
        /// List of unsupported faults.
        /// </summary>
        public Type[] UnsupportedFaults { get; }

        public FaultableAttribute(Type[] supportedFaults = null, Type[] unsupportedFaults = null)
        {
            SupportedFaults = supportedFaults ?? Array.Empty<Type>();
            UnsupportedFaults = unsupportedFaults ?? Array.Empty<Type>();
        }
    }
}
