using SimuNEX.Communication;
using System;
using UnityEngine;

namespace SimuNEX
{
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
