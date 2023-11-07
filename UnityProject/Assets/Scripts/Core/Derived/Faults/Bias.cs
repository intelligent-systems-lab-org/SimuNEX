using System;
using System.Diagnostics;

namespace SimuNEX.Faults.Types
{
    /// <summary>
    /// A fault that adds a constant bias to the value.
    /// </summary>
    [DebuggerDisplay("Bias = {_value}")]
    [Serializable]
    public class Bias : Fault
    {
        /// <summary>
        /// Bias value.
        /// </summary>
        public float _value;

        /// <summary>
        /// <see cref="Bias"/> default constructor.
        /// </summary>
        public Bias()
        {
            _value = 1f;
        }

        /// <summary>
        /// <see cref="Bias"/> constructor.
        /// </summary>
        /// <param name="bias">The bias value.</param>
        public Bias(float bias)
        {
            _value = bias;
        }

        public override float FaultFunction(float val)
        {
            return val + _value;
        }
    }
}
