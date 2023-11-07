using System;
using System.Diagnostics;

namespace SimuNEX.Faults.Types
{
    /// <summary>
    /// A fault that adds gaussian noise to the value.
    /// </summary>
    [DebuggerDisplay("Variance = {variance}")]
    [Serializable]
    public class Gaussian : Fault
    {
        /// <summary>
        /// Variance value.
        /// </summary>
        public float variance;

        /// <summary>
        /// <see cref="Gaussian"/> default constructor.
        /// </summary>
        public Gaussian()
        {
            variance = 0.1f;
        }

        /// <summary>
        /// <see cref="Gaussian"/> constructor.
        /// </summary>
        /// <param name="variance">The variance value.</param>
        public Gaussian(float variance)
        {
            this.variance = variance;
        }

        public override float FaultFunction(float val)
        {
            return val + (UnityEngine.Random.Range(-1, 1) * variance);
        }
    }
}
