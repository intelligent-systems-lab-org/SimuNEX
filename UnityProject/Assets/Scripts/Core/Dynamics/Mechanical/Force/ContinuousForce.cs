namespace SimuNEX.Mechanical
{
    /// <summary>
    /// Applies a constant _force to the attached RigidBody.
    /// </summary>
    public class ContinuousForce : Force
    {
        /// <summary>
        /// 3D _force to apply.
        /// </summary>
        public Vector6DOF forces;

        /// <summary>
        /// Reference frame to apply the _force.
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
