using System;

namespace SimuNEX.Communication
{
    public enum Streaming
    {
        S,
        R,
        SR
    }

    public interface IProtocolInitialization
    {
        void Initialize();
    }

    [Serializable]
    public abstract class COMProtocol
    {
        /// <summary>
        /// Size of the data being sent.
        /// </summary>
        [COMType(Streaming.S)]
        public int sendDataSize;

        /// <summary>
        /// Size of the data being received.
        /// </summary>
        [COMType(Streaming.R)]
        public int receiveDataSize;

        /// <summary>
        /// Creates the <see cref="COMProtocol"/> with the selected settings.
        /// </summary>
        /// <param name="protocol">The communication protocol to use.</param>
        /// <param name="sendDataSize">The size of the data being sent.</param>
        /// <param name="receiveDataSize">The size of the data being received.</param>
        /// <returns>The <see cref="COMProtocol"/> with the selected settings.</returns>
        public static COMProtocol With(COMProtocol protocol, int sendDataSize, int receiveDataSize)
        {
            protocol.sendDataSize = sendDataSize;
            protocol.receiveDataSize = receiveDataSize;
            return protocol;
        }

        /// <summary>
        /// Sends data using the talker.
        /// </summary>
        /// <param name="data">The data to be sent.</param>
        public abstract void Send(in float[] data);

        /// <summary>
        /// Receives data using the listener.
        /// </summary>
        /// <param name="data">The data to be received.</param>
        public abstract void Receive(float[] data);
    }
}
