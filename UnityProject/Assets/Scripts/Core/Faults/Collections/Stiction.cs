using System;
using System.Diagnostics;

namespace SimuNEX.Faults
{
    /// <summary>
    /// A fault that applies a stiction effect to the value.
    /// </summary>
    [DebuggerDisplay("Stiction Threshold = {stictionThreshold}")]
    [Serializable]
    public class Stiction : Fault
    {
        /// <summary>
        /// Stiction threshold value.
        /// </summary>
        public float stictionThreshold;

        /// <summary>
        /// <see cref="Stiction"/> default constructor.
        /// </summary>
        public Stiction()
        {
            stictionThreshold = 75f;
        }

        /// <summary>
        /// <see cref="Stiction"/> constructor.
        /// </summary>
        /// <param name="stictionThreshold">The stiction threshold value.</param>
        public Stiction(float stictionThreshold)
        {
            this.stictionThreshold = stictionThreshold;
        }

        public override float FaultFunction(float val)
        {
            return Math.Abs(val) < stictionThreshold ? 0 : val;
        }
    }
}
