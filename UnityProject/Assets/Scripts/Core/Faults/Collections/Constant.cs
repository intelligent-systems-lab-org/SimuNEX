using System;
using System.Diagnostics;

namespace SimuNEX.Faults
{
    /// <summary>
    /// A fault that sets the value to a constant value.
    /// </summary>
    [DebuggerDisplay("Value = {_value}")]
    [Serializable]
    public class Constant : Fault
    {
        /// <summary>
        /// Constant value to set.
        /// </summary>
        public float _value;

        /// <summary>
        /// <see cref="Constant"/> default constructor.
        /// </summary>
        public Constant()
        {
            _value = 0f;
        }

        /// <summary>
        /// <see cref="Constant"/> constructor.
        /// </summary>
        /// <param name="constantValue">The constant value to set.</param>
        public Constant(float constantValue)
        {
            _value = constantValue;
        }

        public override float FaultFunction(float val)
        {
            return _value;
        }
    }
}
