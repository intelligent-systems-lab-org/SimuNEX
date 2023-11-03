using System;
using System.Diagnostics;

namespace SimuNEX
{
    /// <summary>
    /// A fault that applies a dead zone effect to the value.
    /// </summary>
    [DebuggerDisplay("Dead Zone = {range}")]
    [Serializable]
    public class DeadZoneFault : Fault
    {
        /// <summary>
        /// Dead zone range value. Values within (min, max) will be zeroed out.
        /// </summary>
        public Limits range;

        /// <summary>
        /// <see cref="DeadZoneFault"/> default constructor.
        /// </summary>
        public DeadZoneFault()
        {
            range = new Limits { min = -50f, max = 50f };
        }

        /// <summary>
        /// Creates a <see cref="DeadZoneFault"/> with range (-deadZoneRange, deadZoneRange).
        /// </summary>
        /// <param name="deadZoneRange">The dead zone range value.</param>
        public DeadZoneFault(float deadZoneRange)
        {
            range = new Limits { min = -deadZoneRange, max = deadZoneRange };
        }

        /// <summary>
        /// Creates a <see cref="DeadZoneFault"/> with range (min, max).
        /// </summary>
        /// <param name="min">The lower bound.</param>
        /// <param name="max">The upper bound.</param>
        public DeadZoneFault(float min, float max)
        {
            range = new Limits { min = min, max = max };
        }

        public override float FaultFunction(float val)
        {
            return (val > range.min && val < range.max) ? 0 : val;
        }
    }
}
