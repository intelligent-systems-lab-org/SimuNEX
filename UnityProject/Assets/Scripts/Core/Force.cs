using UnityEngine;

namespace SimuNEX.Mechanical
{
    /// <summary>
    /// Defines a force that can be applied to a <see cref="RBModel"/>.
    /// </summary>
    public abstract class Force : MonoBehaviour
    {
        /// <summary>
        /// Apply the <see cref="Force"/> to the <see cref="RBModel"/>.
        /// </summary>
        public abstract void ApplyForce();
    }
}
