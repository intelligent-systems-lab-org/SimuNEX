using System;
using System.Diagnostics;

namespace SimuNEX
{
    /// <summary>
    /// A fault that adds gaussian noise to the value.
    /// </summary>
    [DebuggerDisplay("Variance = {variance}")]
    [Serializable]
    public class GaussianFault : Fault
    {
        /// <summary>
        /// Variance value.
        /// </summary>
        public float variance;

        /// <summary>
        /// <see cref="GaussianFault"/> default constructor.
        /// </summary>
        public GaussianFault()
        {
            variance = 0.1f;
        }

        /// <summary>
        /// <see cref="GaussianFault"/> constructor.
        /// </summary>
        /// <param name="variance">The variance value.</param>
        public GaussianFault(float variance)
        {
            this.variance = variance;
        }

        protected override float FaultFunction(float val)
        {
            return val + (UnityEngine.Random.Range(-1, 1) * variance);
        }
    }
}
