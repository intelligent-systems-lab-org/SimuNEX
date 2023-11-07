using System;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Interface for implementing numerical stepper methods for <see cref="StateSpace"/>.
    /// </summary>
    public abstract class ODESolver
    {
        /// <summary>
        /// The step size. Defaults to <see cref="Time.fixedDeltaTime"/>.
        /// </summary>
        protected float h = Time.fixedDeltaTime;

        /// <summary>
        /// The step size for numerical integration.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public float stepSize
        {
            get => h;

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Step size must be positive");
                }

                if (value < 0.0025f)
                {
                    throw new ArgumentException(@"Step size must be at least 0.0025 
                        to maintain reasonable performance");
                }

                float remainder = value % 0.005f;
                if (Math.Abs(remainder) > float.Epsilon)
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

    /// <summary>
    /// Contains <see cref="ODESolver"/> functions.
    /// </summary>
    public static class ODESolvers
    {
        /// <summary>
        /// Implements the forward Euler method.
        /// </summary>
        public class ForwardEuler : ODESolver
        {
            public override void Step(StateSpace ss)
            {
                ss.states += h * ss.Derivatives(ss.states, ss.inputs);
            }
        }

        /// <summary>
        /// Implements Heun's method.
        /// </summary>
        public class Heun : ODESolver
        {
            public override void Step(StateSpace ss)
            {
                Matrix k1 = ss.Derivatives(ss.states, ss.inputs);
                Matrix k2 = ss.Derivatives(ss.states + (h * k1), ss.inputs);
                ss.states += h / 2f * (k1 + k2);
            }
        }

        /// <summary>
        /// Implements the 4th-order Runge-Kutta method.
        /// </summary>
        public class RK4 : ODESolver
        {
            public override void Step(StateSpace ss)
            {
                Matrix k1 = h * ss.Derivatives(ss.states, ss.inputs);
                Matrix k2 = h * ss.Derivatives(ss.states + (0.5f * k1), ss.inputs);
                Matrix k3 = h * ss.Derivatives(ss.states + (0.5f * k2), ss.inputs);
                Matrix k4 = h * ss.Derivatives(ss.states + k3, ss.inputs);
                ss.states += 1f / 6f * (k1 + (2f * k2) + (2f * k3) + k4);
            }
        }
    }

    /// <summary>
    /// List of available <see cref="ODESolver"/> methods
    /// </summary>
    public enum StepperMethod
    {
        Euler,
        Heun,
        RK4
    }
}
