using System;
using System.Diagnostics;
using UnityEngine;

namespace SimuNEX
{
    public static partial class FaultTypes
    {
        /// <summary>
        /// A fault that applies quantization error to the value.
        /// </summary>
        [DebuggerDisplay("Quantization Step = {quantizationStep}")]
        [Serializable]
        public class Quantization : Fault
        {
            /// <summary>
            /// Quantization step value.
            /// </summary>
            public float quantizationStep;

            /// <summary>
            /// <see cref="Quantization"/> default constructor.
            /// </summary>
            public Quantization()
            {
                quantizationStep = 0.03125f;
            }

            /// <summary>
            /// <see cref="Quantization"/> constructor.
            /// </summary>
            /// <param name="quantizationStep">The quantization step value.</param>
            public Quantization(float quantizationStep)
            {
                this.quantizationStep = quantizationStep;
            }

            public override float FaultFunction(float val)
            {
                return Mathf.Round(val / quantizationStep) * quantizationStep;
            }
        }
    }
}
