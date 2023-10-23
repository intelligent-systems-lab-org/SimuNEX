using UnityEngine;

/// <summary>
/// Supervisory component for communication.
/// </summary>
public class COMSystem : MonoBehaviour
{
    /// <summary>
    /// Attached <see cref="COMProtocol"/> object.
    /// </summary>
    public COMProtocol protocol;

    private void OnValidate()
    {
        protocol = GetComponent<COMProtocol>();
    }

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