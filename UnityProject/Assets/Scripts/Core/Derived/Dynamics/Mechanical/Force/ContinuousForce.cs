namespace SimuNEX.Mechanical.Forces
{
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
                rigidBody.AddForce(forces.ToICF(transform));
            }
            else
            {
                rigidBody.AddForce(forces);
            }
        }
    }
}
