using UnityEngine;

/// <summary>
/// Defines a force that can be applied to a RigidBody.
/// </summary>
public abstract class Force : MonoBehaviour
{
    /// <summary>
    /// Associated <see cref="RigidBody"/> to apply forces to.
    /// </summary>
    protected RigidBody rb;

    private void OnEnable()
    {
        if (TryGetComponent(out rb))
        {
            rb.AttachForce(this);
        }
        else
        {
            Debug.LogError("RigidBody component not found!", this);
        }
    }

    private void OnDisable()
    {
        rb.RemoveForce(this);
    }

    /// <summary>
    /// Apply the force to the specified RigidBody.
    /// </summary>
    public abstract void ApplyForce();
}