using System;

namespace SimuNEX.Communication
{
    [Serializable]
    public class SelfConnect : COMProtocol
    {
        [COMType(Streaming.S)]
        public float[] sentData;

        [COMType(Streaming.R)]
        public float[] receivedData;

        public override void Receive(ref float[] data)
        {
            for (int i = 0; i < receivedData.Length; i++)
            {
                data[i] = receivedData[i];
            }
        }

        public override void Send(in float[] data)
        {
            for (int i = 0; i < sentData.Length; i++)
            {
                sentData[i] = data[i];
            }
        }
    }
}
