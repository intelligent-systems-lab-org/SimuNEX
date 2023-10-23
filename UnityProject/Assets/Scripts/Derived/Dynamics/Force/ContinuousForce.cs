using UnityEngine;

/// <summary>
/// Applies a constant force to the attached RigidBody.
/// </summary>
public class ContinuousForce : Force
{
    /// <summary>
    /// 3D force to apply.
    /// </summary>
    [SerializeField] public Vector3 forces;

    /// <summary>
    /// 3D torque to apply.
    /// </summary>
    [SerializeField] public Vector3 torques;

    /// <summary>
    /// Reference frame to apply the force.
    /// </summary>
    public CoordinateFrame referenceFrame;

    public override void ApplyForce()
    {
        rb.AddForce(forces, torques, referenceFrame);
    }
}