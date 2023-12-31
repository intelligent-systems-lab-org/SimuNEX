using UnityEngine;

namespace SimuNEX.Mechanical
{
    /// <summary>
    /// Implementation of linear drag.
    /// </summary>
    [DisallowMultipleComponent]
    public class LinearDrag : Force
    {
        /// <summary>
        /// Drag coefficients defined as a <see cref="Matrix6DOF"/>,
        /// where each row refers to a _force applied to a DOF and each column refers to a velocity DOF.
        /// </summary>
        public Matrix6DOF dragCoefficients;

        public override void ApplyForce()
        {
            rigidBody.AddForce(dragCoefficients * rigidBody.velocity * -1);
        }
    }
}
