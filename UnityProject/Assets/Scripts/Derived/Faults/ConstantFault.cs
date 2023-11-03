using System;
using System.Diagnostics;

namespace SimuNEX
{
    /// <summary>
    /// A fault that sets the value to a constant value.
    /// </summary>
    [DebuggerDisplay("Value = {_value}")]
    [Serializable]
    public class ConstantFault : Fault
    {
        /// <summary>
        /// Constant value to set.
        /// </summary>
        public float _value;

        /// <summary>
        /// <see cref="ConstantFault"/> default constructor.
        /// </summary>
        public ConstantFault()
        {
            _value = 0f;
        }

        /// <summary>
        /// <see cref="ConstantFault"/> constructor.
        /// </summary>
        /// <param name="constantValue">The constant value to set.</param>
        public ConstantFault(float constantValue)
        {
            _value = constantValue;
        }

        public override float FaultFunction(float val)
        {
            return _value;
        }
    }
}
