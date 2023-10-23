using ROS2;
using UnityEngine;

[RequireComponent(typeof(ROS2UnityComponent))]
public class ROS2Connector : COMProtocol
{
    private ROS2UnityComponent ros2Unity;
    private ROS2Node outputNode, inputNode;
    public string outputNodeName = "SimuNEXROS2Listener", inputNodeName = "SimuNEXROS2Talker";

    public override void Initialize()
    {
        if (ros2Unity.Ok())
        {
            outputNode ??= ros2Unity.CreateNode(outputNodeName);
            inputNode ??= ros2Unity.CreateNode(inputNodeName);
        }
    }

    public override void Receive(ref float[] data)
    {
        
    }

    public override void Send(float[] data)
    {
        
    }
}
