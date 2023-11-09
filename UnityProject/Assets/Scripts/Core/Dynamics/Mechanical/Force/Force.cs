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
        public RigidBody rigidBody;

        protected void OnEnable()
        {
            TryAttachForce();
        }

        protected void OnDisable()
        {
            TryDetachForce();
        }

        /// <summary>
        /// Attaches the <see cref="Force"/> to the <see cref="RigidBody"/>.
        /// </summary>
        public void TryAttachForce()
        {
            if (rigidBody != null || TryGetComponent(out rigidBody))
            {
                rigidBody.AttachForce(this);
            }
            else
            {
                Debug.LogWarning("RigidBody component not found!", this);
            }
        }

        /// <summary>
        /// Removes the <see cref="Force"/> from the <see cref="RigidBody"/>.
        /// </summary>
        public void TryDetachForce()
        {
            if (rigidBody != null)
            {
                rigidBody.RemoveForce(this);
            }
        }

        /// <summary>
        /// Apply the <see cref="Force"/> to the <see cref="RigidBody"/>.
        /// </summary>
        public abstract void ApplyForce();
    }
}
