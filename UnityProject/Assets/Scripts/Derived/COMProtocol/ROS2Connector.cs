using ROS2;
using std_msgs.msg;
using UnityEngine;

[RequireComponent(typeof(ROS2UnityComponent))]
public class ROS2Connector : COMProtocol
{
    private ROS2UnityComponent ros2Unity;
    private ROS2Node outputNode, inputNode;
    public string outputNodeName = "SimuNEXROS2Listener", inputNodeName = "SimuNEXROS2Talker";
    public string publisherName = "SystemOutputs", subscriberName = "SystemInputs";
    private IPublisher<Float32MultiArray> outputPublisher;
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
            inputSubscriber ??= inputNode.CreateSubscription<Float32MultiArray>(subscriberName, msg =>
            {
                for (int i = 0; i < msg.Data.Length; ++i)
                {
                    data[i] = msg.Data[i];
                }
            });
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