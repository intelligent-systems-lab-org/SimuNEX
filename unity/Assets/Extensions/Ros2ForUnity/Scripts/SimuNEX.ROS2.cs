using ROS2;
using std_msgs.msg;
using System;
using UnityEngine;

namespace SimuNEX.Communication
{
    /// <summary>
    /// Provides an interface for SimuNEX to connect and communicate with ROS2 systems.
    /// </summary>
    [Serializable]
    public class ROS2 : COMProtocol, IProtocolInitialization
    {
        /// <summary>
        /// Reference to the attached ROS2 Unity component for managing ROS2 operations.
        /// </summary>
        public ROS2UnityComponent Component;

        /// <summary>
        /// Node dedicated to sending out ROS2 messages.
        /// </summary>
        private ROS2Node _node;

        /// <summary>
        /// Name of the ROS2 node.
        /// </summary>
        public string node = "SimuNEXROS2Node";

        /// <summary>
        /// Name of the publisher for system outputs.
        /// </summary>
        [COMType(Streaming.S)]
        public string publisherName = "SystemOutputs";

        /// <summary>
        /// Name of the subscriber for system inputs.
        /// </summary>
        [COMType(Streaming.R)]
        public string subscriberName = "SystemInputs";

        /// <summary>
        /// Publisher responsible for sending out ROS2 messages.
        /// </summary>
        [COMType(Streaming.S)]
        private IPublisher<Float32MultiArray> publisher;

        /// <summary>
        /// Subscriber responsible for listening to incoming ROS2 messages.
        /// </summary>
        [COMType(Streaming.R)]
        private ISubscription<Float32MultiArray> subscriber;

        public void Initialize()
        {
            if (Component.Ok())
            {
                _node ??= Component.CreateNode(node);
                Debug.Log($"Success: ROS2 Node {_node.name} created");
            }
        }

        public override void Receive(float[] data)
        {
            if (Component.Ok())
            {
                subscriber ??= _node
                    .CreateSubscription<Float32MultiArray>
                    (
                        subscriberName,
                        msg => Array.Copy(msg.Data, data, msg.Data.Length)
                    );
            }
        }

        public override void Send(in float[] data)
        {
            if (Component.Ok())
            {
                publisher ??= _node.CreatePublisher<Float32MultiArray>(publisherName);
                Float32MultiArray msg = new() { Data = data };
                publisher.Publish(msg);
            }
        }
    }
}
