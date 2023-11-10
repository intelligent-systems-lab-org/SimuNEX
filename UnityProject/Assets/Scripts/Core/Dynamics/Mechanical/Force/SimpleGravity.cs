namespace SimuNEX.Mechanical
{
    /// <summary>
    /// Implementation of a simple gravity _force.
    /// </summary>
    [SingleInstance]
    public class SimpleGravity : PointForce
    {
        /// <summary>
        /// The acceleration due to gravity.
        /// </summary>
        public float acceleration = 9.81f;

        /// <summary>
        /// Calculate the weight of the specified <see cref="RigidBody"/> object.
        /// </summary>
        /// <returns>The weight of the dynamics object.</returns>
        public float weight => rigidBody.mass * acceleration;
        protected override float force => weight;
        protected override string centerName => "COG";
        protected override Direction direction => Direction.Down;
    }
}
