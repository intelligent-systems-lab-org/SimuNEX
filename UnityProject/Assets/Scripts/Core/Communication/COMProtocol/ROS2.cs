using ROS2;
using std_msgs.msg;
using System;

namespace SimuNEX.Communication
{
    /// <summary>
    /// Provides an interface for SimuNEX to connect and communicate with ROS2 systems.
    /// </summary>
    [Serializable]
    public class ROS2 : COMProtocol
    {
        /// <summary>
        /// Reference to the attached ROS2 Unity component for managing ROS2 operations.
        /// </summary>
        public ROS2UnityComponent ros2;

        /// <summary>
        /// Node dedicated to sending out ROS2 messages.
        /// </summary>
        private ROS2Node outputNode;

        /// <summary>
        /// Node dedicated to listening to incoming ROS2 messages.
        /// </summary>
        private ROS2Node inputNode;

        /// <summary>
        /// Name of the node responsible for listening to ROS2 messages.
        /// </summary>
        public string outputNodeName = "SimuNEXROS2Listener";

        /// <summary>
        /// Name of the node responsible for sending out ROS2 messages.
        /// </summary>
        public string inputNodeName = "SimuNEXROS2Talker";

        /// <summary>
        /// Name of the publisher for system outputs.
        /// </summary>
        public string publisherName = "SystemOutputs";

        /// <summary>
        /// Name of the subscriber for system inputs.
        /// </summary>
        public string subscriberName = "SystemInputs";

        /// <summary>
        /// Publisher responsible for sending out ROS2 messages.
        /// </summary>
        public IPublisher<Float32MultiArray> publisher;

        /// <summary>
        /// Subscriber responsible for listening to incoming ROS2 messages.
        /// </summary>
        public ISubscription<Float32MultiArray> subscriber;

        public override void Receive(float[] data)
        {
            throw new System.NotImplementedException();
        }

        public override void Send(in float[] data)
        {
            throw new System.NotImplementedException();
        }
    }
}
