using System;
using System.Diagnostics;

namespace SimuNEX
{
    public static partial class FaultTypes
    {
        /// <summary>
        /// A fault that scales the value.
        /// </summary>
        [DebuggerDisplay("Gain = {gain}")]
        [Serializable]
        public class Scale : Fault
        {
            /// <summary>
            /// Gain value.
            /// </summary>
            public float gain;

            /// <summary>
            /// <see cref="Scale"/> default constructor.
            /// </summary>
            public Scale()
            {
                gain = 1.1f;
            }

            /// <summary>
            /// <see cref="Scale"/> constructor.
            /// </summary>
            /// <param name="gain">The gain value.</param>
            public Scale(float gain)
            {
                this.gain = gain;
            }

            public override float FaultFunction(float val)
            {
                return val * gain;
            }
        }
    }
}
