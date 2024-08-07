using System;

namespace SimuNEX.Communication
{
    public enum Streaming
    {
        S,
        R,
        SR
    }

    [Serializable]
    public abstract class COMProtocol
    {
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
