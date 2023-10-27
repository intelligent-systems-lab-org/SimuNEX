using UnityEngine;

/// <summary>
/// Interface for describing dynamic systems.
/// </summary>
[RequireComponent(typeof(DynamicSystem))]
public abstract class Dynamics : MonoBehaviour
{
    /// <summary>
    /// Configures the system at the start of the physics simulation.
    /// </summary>
    protected abstract void Initialize();

    /// <summary>
    /// Applys the accumulated inputs to the system.
    /// </summary>
    public abstract void Step();
}
