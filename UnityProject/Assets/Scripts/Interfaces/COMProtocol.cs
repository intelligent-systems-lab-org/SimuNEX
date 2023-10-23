using ROS2;
using UnityEngine;

public abstract class COMProtocol : MonoBehaviour
{
    public abstract void Initialize();

    private void OnValidate()
    {
        Initialize();
    }

    public abstract void Send(float[] data);
    public abstract void Receive(ref float[] data);
}