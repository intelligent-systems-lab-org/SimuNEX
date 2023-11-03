using System;
using System.Diagnostics;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// A fault that applies quantization error to the value.
    /// </summary>
    [DebuggerDisplay("Quantization Step = {quantizationStep}")]
    [Serializable]
    public class QuantizationFault : Fault
    {
        /// <summary>
        /// Quantization step value.
        /// </summary>
        public float quantizationStep;

        /// <summary>
        /// <see cref="QuantizationFault"/> default constructor.
        /// </summary>
        public QuantizationFault()
        {
            quantizationStep = 0.03125f;
        }

        /// <summary>
        /// <see cref="QuantizationFault"/> constructor.
        /// </summary>
        /// <param name="quantizationStep">The quantization step value.</param>
        public QuantizationFault(float quantizationStep)
        {
            this.quantizationStep = quantizationStep;
        }

        protected override float FaultFunction(float val)
        {
            return Mathf.Round(val / quantizationStep) * quantizationStep;
        }
    }
}
