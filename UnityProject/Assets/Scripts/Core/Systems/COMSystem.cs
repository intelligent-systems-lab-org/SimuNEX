using SimuNEX.Communication;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Supervisory component for communication.
    /// </summary>
    [DisallowMultipleComponent]
    public class COMSystem : MonoBehaviour
    {
        /// <summary>
        /// Attached <see cref="COMProtocol"/> object.
        /// </summary>
        public COMProtocol protocol;

        protected void OnValidate() => Setup();
        protected void OnEnable() => Setup();

        /// <summary>
        /// Sets up the <see cref="protocol"/> object.
        /// </summary>
        public void Setup() 
        {
            protocol = GetComponent<COMProtocol>();
        }

        /// <summary>
        /// Sends data using the <see cref="COMProtocol"/>.
        /// </summary>
        /// <param name="data">Data to be sent.</param>
        public void Send(float[] data)
        {
            if (protocol != null)
            {
                protocol.Send(data);
            }
            else
            {
                Debug.LogWarning("COMProtocol component not found!");
            }
        }

        /// <summary>
        /// Receives data using the <see cref="COMProtocol"/>.
        /// </summary>
        /// <param name="data">Data to be received.</param>
        public void Receive(float[] data)
        {
            if (protocol != null)
            {
                protocol.Receive(data);
            }
            else
            {
                Debug.LogWarning("COMProtocol component not found!");
            }
        }
    }
}
