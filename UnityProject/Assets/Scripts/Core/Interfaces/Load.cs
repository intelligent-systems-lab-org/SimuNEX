using System;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Generalizable class for any load that an actuator might drive.
    /// </summary>
    public abstract class Load : MonoBehaviour
    {
        /// <summary>
        /// Function of actuator attached to load.
        /// </summary>
        protected Func<float> actuatorFunction;

        /// <summary>
        /// Attached <see cref="RigidBody"/> to apply forces to.
        /// </summary>
        public Mechanical.RigidBody rigidBody;

        /// <summary>
        /// Attaches an <see cref="Actuator"/>.
        /// </summary>
        /// <param name="actuatorFunction">Function associated with the <see cref="Actuator"/> object.</param>
        public void AttachActuator(Func<float> actuatorFunction)
        {
            this.actuatorFunction = actuatorFunction;
        }

        /// <summary>
        /// Detaches an <see cref="Actuator"/>.
        /// </summary>
        public void DetachActuator()
        {
            actuatorFunction = null;
        }
    }
}
