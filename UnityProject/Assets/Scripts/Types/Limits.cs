using System;
using System.Diagnostics;

namespace SimuNEX
{
    /// <summary>
    /// For defining quantities with bounds.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Min = {min}, Max = {max}")]
    public struct Limits
    {
        public float max;
        public float min;
    }
}
