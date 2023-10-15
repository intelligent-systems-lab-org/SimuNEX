using UnityEngine;

/// <summary>
/// Defines a force that can be applied to a RigidBody.
/// </summary>
[RequireComponent(typeof(RigidBody))]
public abstract class Force : MonoBehaviour
{
    protected RigidBody rb;

    private void OnEnable()
    {
        rb = GetComponent<RigidBody>();
        rb.AttachForce(this);
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
