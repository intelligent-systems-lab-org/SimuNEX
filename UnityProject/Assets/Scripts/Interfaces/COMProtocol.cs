using UnityEngine;

namespace SimuNEX 
{
    /// <summary>
    /// Interface for communication protocols (eg., TCP/IP, UDP, RTPS, MQTT, Simplex, etc).
    /// </summary>
    public abstract class COMProtocol : MonoBehaviour
    {
        /// <summary>
        /// Initializes protocol for communication such as setting up listeners and talkers.
        /// </summary>
        public abstract void Initialize();

        private void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// Sends data using the talker.
        /// </summary>
        /// <param name="data">The data to be sent.</param>
        public abstract void Send(float[] data);

        /// <summary>
        /// Receives data using the listener.
        /// </summary>
        /// <param name="data">The data to be received.</param>
        public abstract void Receive(float[] data);
    }
}