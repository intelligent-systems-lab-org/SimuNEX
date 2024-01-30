using System;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Uses a boolean variable named <see cref="OmitFieldName"/> to mark a field as omittable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class OmittableAttribute : PropertyAttribute
    {
        /// <summary>
        /// Name of the boolean variable that controls visibility.
        /// </summary>
        public string OmitFieldName { get; }

        public OmittableAttribute(string omitFieldName = null)
        {
            OmitFieldName = omitFieldName;
        }
    }

    /// <summary>
    /// Marks the variable as a parameter to the system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ParameterAttribute : OmittableAttribute
    {
        public ParameterAttribute(string omitFieldName = null)
            : base(omitFieldName)
        {
        }
    }

    /// <summary>
    /// Marks the variable as an input to the system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class InputAttribute : OmittableAttribute
    {
        public InputAttribute(string omitFieldName = null)
            : base(omitFieldName)
        {
        }
    }

    /// <summary>
    /// Marks the variable as an output to the system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class OutputAttribute : OmittableAttribute
    {
        public OutputAttribute(string omitFieldName = null)
            : base(omitFieldName)
        {
        }
    }

    /// <summary>
    /// Marks the variable as a sound property related to the system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class SFXAttribute : PropertyAttribute
    {
    }

    /// <summary>
    /// Marks the variable as a constraint.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ConstraintAttribute : PropertyAttribute
    {
    }

    /// <summary>
    /// Marks the variable as a solver.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class SolverAttribute : PropertyAttribute
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

        /// <summary>
        /// Sets either a list of supported and unsupported fault operations on the field.
        /// </summary>
        /// <param name="supportedFaults">List of faults that the field allows.</param>
        /// <param name="unsupportedFaults">List of faults that the field does not allow.</param>
        public FaultableAttribute(Type[] supportedFaults = null, Type[] unsupportedFaults = null)
        {
            SupportedFaults = supportedFaults ?? Array.Empty<Type>();
            UnsupportedFaults = unsupportedFaults ?? Array.Empty<Type>();
        }
    }
}
