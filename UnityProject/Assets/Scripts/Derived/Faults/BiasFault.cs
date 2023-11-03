using System;
using System.Diagnostics;

namespace SimuNEX
{
    /// <summary>
    /// A fault that adds a constant bias to the value.
    /// </summary>
    [DebuggerDisplay("Bias = {bias}")]
    [Serializable]
    public class BiasFault : Fault
    {
        /// <summary>
        /// Bias value.
        /// </summary>
        public float bias;

        /// <summary>
        /// <see cref="BiasFault"/> default constructor.
        /// </summary>
        public BiasFault()
        {
            bias = 1f;
        }

        /// <summary>
        /// <see cref="BiasFault"/> constructor.
        /// </summary>
        /// <param name="bias">The bias value.</param>
        public BiasFault(float bias)
        {
            this.bias = bias;
        }

        protected override float FaultFunction(float val)
        {
            return val + bias;
        }
    }
}
