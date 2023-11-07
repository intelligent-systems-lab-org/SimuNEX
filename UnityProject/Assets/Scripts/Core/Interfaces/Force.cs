using UnityEngine;

namespace SimuNEX.Mechanical
{
    /// <summary>
    /// Defines a force that can be applied to a RigidBody.
    /// </summary>
    public abstract class Force : MonoBehaviour
    {
        /// <summary>
        /// Associated <see cref="RigidBody"/> to apply forces to.
        /// </summary>
        protected RigidBody rigidBody;

        protected void OnEnable()
        {
            if (TryGetComponent(out rigidBody))
            {
                rigidBody.AttachForce(this);
            }
            else
            {
                Debug.LogError("RigidBody component not found!", this);
            }
        }

        protected void OnDisable()
        {
            rigidBody.RemoveForce(this);
        }

        /// <summary>
        /// Apply the force to the specified RigidBody.
        /// </summary>
        public abstract void ApplyForce();
    }
}
