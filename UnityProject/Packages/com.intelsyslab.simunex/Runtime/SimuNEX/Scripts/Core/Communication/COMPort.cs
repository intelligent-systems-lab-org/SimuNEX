using System;

namespace SimuNEX.Communication
{
    [Serializable]
    public class COMInput : Port
    {
        public COMInput(string name, int size = 1)
            : base(name, size, Signal.Data)
        {
        }
    }

    [Serializable]
    public class COMOutput : Port
    {
        public COMOutput(string name, int size = 1)
            : base(name, size, Signal.Data)
        {
        }
    }
}
