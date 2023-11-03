using System;
using System.Diagnostics;

namespace SimuNEX
{
    /// <summary>
    /// A fault that adds a constant bias to the value.
    /// </summary>
    [DebuggerDisplay("Bias = {_value}")]
    [Serializable]
    public class BiasFault : Fault
    {
        /// <summary>
        /// Bias value.
        /// </summary>
        public float _value;

        /// <summary>
        /// <see cref="BiasFault"/> default constructor.
        /// </summary>
        public BiasFault()
        {
            _value = 1f;
        }

        /// <summary>
        /// <see cref="BiasFault"/> constructor.
        /// </summary>
        /// <param name="bias">The bias value.</param>
        public BiasFault(float bias)
        {
            _value = bias;
        }

        public override float FaultFunction(float val)
        {
            return val + _value;
        }
    }
}
