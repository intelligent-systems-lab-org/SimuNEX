using System;

namespace SimuNEX.Communication
{
    [Serializable]
    public class COMInput : ModelInput
    {
        public COMInput(string name, int size = 1, IBlock connectedBlock = null)
            : base(name, size, Signal.Data, connectedBlock)
        {
        }
    }

    [Serializable]
    public class COMOutput : ModelOutput
    {
        public COMOutput(string name, int size = 1, IBlock connectedBlock = null)
            : base(name, size, Signal.Data, connectedBlock)
        {
        }
    }
}
