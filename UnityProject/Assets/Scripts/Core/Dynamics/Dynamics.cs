using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Interface for describing dynamic systems.
    /// </summary>
    [RequireComponent(typeof(DynamicSystem))]
    public abstract class Dynamics : MonoBehaviour
    {
        /// <summary>
        /// Applies initial conditions at the start of the physics simulation.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Applys the accumulated inputs to the system.
        /// </summary>
        public abstract void Step();

        /// <summary>
        /// Resets all states to their defaults.
        /// </summary>
        public abstract void Reset();
    }
}
