/// <summary>
/// Applies a constant force to the attached RigidBody.
/// </summary>
public class ContinuousForce : Force
{
    /// <summary>
    /// 3D force to apply.
    /// </summary>
    public Vector6DOF forces;

    /// <summary>
    /// Reference frame to apply the force.
    /// </summary>
    public CoordinateFrame referenceFrame;

    public override void ApplyForce()
    {
        if (referenceFrame == CoordinateFrame.BCF)
        {
            rb.AddForce(forces.ToBodyFrame(transform));
        }
        else
        {
            rb.AddForce(forces);
        }
    }
}