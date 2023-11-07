using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Implementation of a simple gravity force.
    /// </summary>
    [SingleInstance]
    public class SimpleGravity : Force
    {
        /// <summary>
        /// The acceleration due to gravity.
        /// </summary>
        public float acceleration = 9.81f;

        /// <summary>
        /// The center of gravity.
        /// </summary>
        public Transform centerOfGravity;

        protected void OnValidate()
        {
            FindCOG();
        }

        protected void Awake()
        {
            FindCOG();
        }

        /// <summary>
        /// Attempts to find a child with the name "COG" and assigns it as the COG.
        /// </summary>
        private void FindCOG()
        {
            if (centerOfGravity == null)
            {
                Transform potentialCOG = transform.Find("COG");
                if (potentialCOG != null)
                {
                    centerOfGravity = potentialCOG;
                }
            }
        }

        /// <summary>
        /// Apply the gravity force to the specified <see cref="RigidBody"/> object.
        /// </summary>
        public override void ApplyForce()
        {
            Vector3 gravityForce = Vector3.down * weight;
            rigidBody.AddLinearForceAtPosition(gravityForce, centerOfGravity.position);
        }

        /// <summary>
        /// Calculate the weight of the specified <see cref="RigidBody"/> object.
        /// </summary>
        /// <returns>The weight of the dynamics object.</returns>
        public float weight => rigidBody.mass * acceleration;
    }
}
