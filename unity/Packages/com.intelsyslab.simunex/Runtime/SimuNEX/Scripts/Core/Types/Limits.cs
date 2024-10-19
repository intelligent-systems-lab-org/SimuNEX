using System;
using System.Diagnostics;

namespace SimuNEX
{
    /// <summary>
    /// For defining quantities with bounds.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Min = {min}, Max = {max}")]
    public struct Limits
    {
        public float max;
        public float min;
    }

    public static class LimitsExtensions
    {
        /// <summary>
        /// Returns the range of the limits.
        /// </summary>
        /// <returns>The range of the limits.</returns>
        public static float Range(this Limits limits)
        {
            return limits.max - limits.min;
        }

        /// <summary>
        /// Clamps a value to the limits.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(this float value, Limits limits)
        {
            return Math.Clamp(value, limits.min, limits.max);
        }

        /// <summary>
        /// Rolls over a value to the limits.
        /// </summary>
        /// <param name="value">The value to roll over.</param>
        /// <returns>The rolled over value.</returns>
        public static float Modulo(this Limits limits, float value)
        {
            return (value - limits.min) % limits.Range() + limits.min;
        }
    }
}
