using System;

namespace SimuNEX.Communication
{
    /// <summary>
    /// Simple <see cref="COMProtocol"/> that handles communication within Unity.
    /// </summary>
    [Serializable]
    public class SelfConnect : COMProtocol, IProtocolInitialization
    {
        [COMType(Streaming.S)]
        public float[] sentData;

        [COMType(Streaming.R)]
        public float[] receivedData;

        public void Initialize()
        {
            sentData = new float[sendDataSize];
            receivedData = new float[receiveDataSize];
        }

        public override void Receive(float[] data)
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
