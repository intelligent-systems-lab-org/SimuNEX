using UnityEngine;

namespace SimuNEX.Mechanical
{
    /// <summary>
    /// Interface for handling environmental-based forces.
    /// </summary>
    public abstract class ForceField : MonoBehaviour
    {
        /// <summary>
        /// Applies environment-specific effects to the <see cref="RigidBody"/>.
        /// </summary>
        /// <param name="rigidBody">The <see cref="RigidBody"/> inside the field.</param>
        public abstract void Apply(RigidBody rigidBody);

        /// <summary>
        /// Removes the environment-specific effects applied to the <see cref="RigidBody"/>.
        /// </summary>
        /// <param name="rigidBody">The <see cref="RigidBody"/> about to leave the field.</param>
        public abstract void Remove(RigidBody rigidBody);
    }
}
