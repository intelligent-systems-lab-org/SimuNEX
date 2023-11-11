using UnityEngine;

namespace SimuNEX.Mechanical
{
    /// <summary>
    /// Implementation of quadratic drag where the _force depends on the square of the velocity of the <see cref="RigidBody"/>.
    /// </summary>
    [DisallowMultipleComponent]
    public class QuadraticDrag : Force
    {
        /// <summary>
        /// Drag coefficients defined as a <see cref="Matrix6DOF"/>,
        /// where each row refers to a _force applied to a DOF and each column refers to a velocity DOF.
        /// </summary>
        public Matrix6DOF dragCoefficients = new();

        public override void ApplyForce()
        {
            rigidBody.AddForce(dragCoefficients * rigidBody.velocity.Apply(v => Mathf.Abs(v) * v) * -1);
        }
    }
}
