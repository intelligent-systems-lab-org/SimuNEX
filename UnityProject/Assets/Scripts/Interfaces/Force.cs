using UnityEngine;

/// <summary>
/// Defines a force that can be applied to a RigidBody.
/// </summary>
public abstract class Force : MonoBehaviour
{
    protected RigidBody rb;

    private void OnEnable()
    {
        rb = GetComponent<RigidBody>();
        if (rb != null)
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
