using System;

namespace SimuNEX.Communication
{
    /// <summary>
    /// Simple <see cref="COMProtocol"/> that handles communication within Unity.
    /// </summary>
    [Serializable]
    public class SelfConnect : COMProtocol
    {
        [COMType(Streaming.S)]
        public float[] sentData;

        [COMType(Streaming.R)]
        public float[] receivedData;

        public override void Receive(float[] data)
        {
            if (receivedData == null || data.Length != receivedData.Length)
            {
                receivedData = new float[data.Length];
            }

            for (int i = 0; i < receivedData.Length; i++)
            {
                data[i] = receivedData[i];
            }
        }

        public override void Send(in float[] data)
        {
            if (sentData == null || data.Length != sentData.Length)
            {
                sentData = new float[data.Length];
            }

            for (int i = 0; i < sentData.Length; i++)
            {
                sentData[i] = data[i];
            }
        }
    }
}
