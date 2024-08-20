using SimuNEX.Communication;
using System;
using UnityEngine;

namespace SimuNEX
{
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

    /// <summary>
    /// Marks the property as visible depending on <see cref="Streaming"/> mode.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class COMTypeAttribute : PropertyAttribute
    {
        /// <summary>
        /// The streaming direction that the property should be used for.
        /// </summary>
        public Streaming StreamingMode { get; }

        /// <summary>
        /// Creates a <see cref="COMTypeAttribute"/> with the specific <see cref="Streaming"/> mode.
        /// </summary>
        /// <param name="streamingMode">The streaming mode (S, R, SR).</param>
        public COMTypeAttribute(Streaming streamingMode)
        {
            StreamingMode = streamingMode;
        }
    }
}
