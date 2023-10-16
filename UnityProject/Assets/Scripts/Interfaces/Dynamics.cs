using UnityEngine;

/// <summary>
/// Interface for describing dynamic systems.
/// </summary>
public abstract class Dynamics : MonoBehaviour
{
    /// <summary>
    /// Applys the accumulated inputs to the system.
    /// </summary>
    protected abstract void Step();
}
