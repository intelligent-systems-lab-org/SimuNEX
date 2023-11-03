using System;
using System.Diagnostics;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// A fault that sets the value to a constant value.
    /// </summary>
    [DebuggerDisplay("Value = {constantValue}")]
    [Serializable]
    public class ConstantFault : Fault
    {
        /// <summary>
        /// Constant value to set.
        /// </summary>
        public float constantValue;

        /// <summary>
        /// <see cref="ConstantFault"/> default constructor.
        /// </summary>
        public ConstantFault()
        {
            constantValue = 0f;
        }

        /// <summary>
        /// <see cref="ConstantFault"/> constructor.
        /// </summary>
        /// <param name="constantValue">The constant value to set.</param>
        public ConstantFault(float constantValue)
        {
            this.constantValue = constantValue;
        }

        protected override float FaultFunction(float val)
        {
            return constantValue;
        }
    }
}
