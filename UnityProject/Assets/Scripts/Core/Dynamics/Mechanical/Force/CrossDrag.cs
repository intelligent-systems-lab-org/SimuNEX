using UnityEngine;

namespace SimuNEX.Mechanical
{
    /// <summary>
    /// Implementation of cross drag where the _force depends on the product pairwise velocity terms of the <see cref="RigidBody"/>.
    /// </summary>
    [DisallowMultipleComponent]
    public class CrossDrag : Force
    {
        /// <summary>
        /// Drag coefficients defined as a 6 x 15 <see cref="Matrix"/>,
        /// where each row refers to a _force applied to a DOF and each column refers to a pairwise velocity,
        /// whose order is uv, uw, up, uq, ur, vw, vp, vq, vr, wp, wq, wr, pq, pr, qr.
        /// </summary>
        public Matrix dragCoefficients = new(6, 15);

        private Matrix pairwise_velocities = new(15, 1);

        public override void ApplyForce()
        {
            pairwise_velocities[0, 0] = rigidBody.velocity.u * rigidBody.velocity.v;
            pairwise_velocities[1, 0] = rigidBody.velocity.u * rigidBody.velocity.w;
            pairwise_velocities[2, 0] = rigidBody.velocity.u * rigidBody.velocity.p;
            pairwise_velocities[3, 0] = rigidBody.velocity.u * rigidBody.velocity.q;
            pairwise_velocities[4, 0] = rigidBody.velocity.u * rigidBody.velocity.r;

            pairwise_velocities[5, 0] = rigidBody.velocity.v * rigidBody.velocity.w;
            pairwise_velocities[6, 0] = rigidBody.velocity.v * rigidBody.velocity.p;
            pairwise_velocities[7, 0] = rigidBody.velocity.v * rigidBody.velocity.q;
            pairwise_velocities[8, 0] = rigidBody.velocity.v * rigidBody.velocity.r;

            pairwise_velocities[9, 0] = rigidBody.velocity.w * rigidBody.velocity.p;
            pairwise_velocities[10, 0] = rigidBody.velocity.w * rigidBody.velocity.q;
            pairwise_velocities[11, 0] = rigidBody.velocity.w * rigidBody.velocity.r;

            pairwise_velocities[12, 0] = rigidBody.velocity.p * rigidBody.velocity.q;
            pairwise_velocities[13, 0] = rigidBody.velocity.p * rigidBody.velocity.r;
            pairwise_velocities[14, 0] = rigidBody.velocity.q * rigidBody.velocity.r;

            rigidBody.AddForce(dragCoefficients * pairwise_velocities);
        }
    }
}
