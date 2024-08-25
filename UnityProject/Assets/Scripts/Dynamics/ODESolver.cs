using System;
using UnityEngine;

namespace SimuNEX.Dynamics
{
    /// <summary>
    /// Interface for implementing numerical stepper methods for <see cref="StateSpace"/>.
    /// </summary>
    public abstract class ODESolver
    {
        /// <summary>
        /// The step size. Defaults to <see cref="Time.fixedDeltaTime"/>.
        /// </summary>
        private float h = -1;

        /// <summary>
        /// The step size for numerical integration.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public float stepSize
        {
            get => h == -1f ? Time.fixedDeltaTime : h;

            set
            {
                if (value is <= 0 and not (-1))
                {
                    throw new ArgumentException("Step size must be positive");
                }

                if (value is < 0.0025f and not (-1))
                {
                    throw new ArgumentException(@"Step size must be at least 0.0025 
                        to maintain reasonable performance");
                }

                float remainder = value % 0.005f;
                if (Math.Abs(remainder) > float.Epsilon && value != -1)
                {
                    throw new ArgumentException("Step size must be a multiple of 0.005");
                }

                h = value;
            }
        }

        /// <summary>
        /// Numerically integrates the <see cref="StateSpace.DerivativeFunction"/>.
        /// </summary>
        /// <param name="ss"><see cref="StateSpace"/> to be integrated.</param>
        public abstract void Step(StateSpace ss);
    }
}
