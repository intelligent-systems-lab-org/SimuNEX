namespace SimuNEX
{
    /// <summary>
    /// Interface for modeling force-based systems, such as rigidbodies and kinematic chains.
    /// </summary>
    public abstract class Mechanical : Dynamics
    {
        // WIP
    }

    /// <summary>
    /// Represents the coordinate frame used for a vector in a 6DOF space.
    /// </summary>
    public enum CoordinateFrame
    {
        /// <summary>
        /// Body-Centered Frame (BCF) represents the coordinate frame relative to the body or local frame.
        /// </summary>
        BCF,

        /// <summary>
        /// Inertial Coordinate Frame (ICF) represents the coordinate frame fixed in an inertial reference frame.
        /// </summary>
        ICF
    }
}