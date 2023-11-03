using System;
using System.Diagnostics;

namespace SimuNEX
{
    /// <summary>
    /// A fault that applies a stiction effect to the value.
    /// </summary>
    [DebuggerDisplay("Stiction Threshold = {stictionThreshold}")]
    [Serializable]
    public class StictionFault : Fault
    {
        /// <summary>
        /// Stiction threshold value.
        /// </summary>
        public float stictionThreshold;

        /// <summary>
        /// <see cref="StictionFault"/> default constructor.
        /// </summary>
        public StictionFault()
        {
            stictionThreshold = 75f;
        }

        /// <summary>
        /// <see cref="StictionFault"/> constructor.
        /// </summary>
        /// <param name="stictionThreshold">The stiction threshold value.</param>
        public StictionFault(float stictionThreshold)
        {
            this.stictionThreshold = stictionThreshold;
        }

        protected override float FaultFunction(float val)
        {
            return Math.Abs(val) < stictionThreshold ? 0 : val;
        }
    }
}
