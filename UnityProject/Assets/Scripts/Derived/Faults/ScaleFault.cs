using System;
using System.Diagnostics;

namespace SimuNEX
{
    /// <summary>
    /// A fault that scales the value.
    /// </summary>
    [DebuggerDisplay("Gain = {gain}")]
    [Serializable]
    public class ScaleFault : Fault
    {
        /// <summary>
        /// Gain value.
        /// </summary>
        public float gain;

        /// <summary>
        /// <see cref="ScaleFault"/> default constructor.
        /// </summary>
        public ScaleFault()
        {
            gain = 1.1f;
        }

        /// <summary>
        /// <see cref="ScaleFault"/> constructor.
        /// </summary>
        /// <param name="gain">The gain value.</param>
        public ScaleFault(float gain)
        {
            this.gain = gain;
        }

        protected override float FaultFunction(float val)
        {
            return val * gain;
        }
    }
}
