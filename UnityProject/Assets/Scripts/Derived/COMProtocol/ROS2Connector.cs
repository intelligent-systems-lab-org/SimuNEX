using ROS2;
using std_msgs.msg;
using System;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Provides an interface for SimuNEX to connect and communicate with ROS2.
    /// </summary>
    [RequireComponent(typeof(ROS2UnityComponent))]
    public class ROS2Connector : COMProtocol
    {
        /// <summary>
        /// Reference to the attached ROS2 Unity component for managing ROS2 operations.
        /// </summary>
        private ROS2UnityComponent ros2Unity;

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
        private IPublisher<Float32MultiArray> outputPublisher;

        /// <summary>
        /// Subscriber responsible for listening to incoming ROS2 messages.
        /// </summary>
        private ISubscription<Float32MultiArray> inputSubscriber;

        public override void Initialize()
        {
            ros2Unity = GetComponent<ROS2UnityComponent>();

            if (ros2Unity.Ok())
            {
                outputNode ??= ros2Unity.CreateNode(outputNodeName);
                inputNode ??= ros2Unity.CreateNode(inputNodeName);
            }
        }

        public override void Receive(float[] data)
        {
            if (ros2Unity.Ok())
            {
                inputSubscriber ??= inputNode.CreateSubscription<Float32MultiArray>(
                    subscriberName,
                    msg => Array.Copy(msg.Data, data, msg.Data.Length));
            }
        }

        public override void Send(float[] data)
        {
            if (ros2Unity.Ok())
            {
                outputPublisher ??= outputNode.CreatePublisher<Float32MultiArray>(publisherName);
                Float32MultiArray msg = new() { Data = data };
                outputPublisher.Publish(msg);
            }
        }
    }
}