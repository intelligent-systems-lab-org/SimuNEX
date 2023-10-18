using System;
using UnityEngine;

/// <summary>
/// Interface for implementing numerical integrators for <see cref="StateSpace"/>.
/// </summary>
public abstract class Integrator
{
    /// <summary>
    /// The step size. Defaults to <see cref="Time.fixedDeltaTime"/>.
    /// </summary>
    protected float h = Time.fixedDeltaTime;

    /// <summary>
    /// The step size for numerical integration.
    /// </summary>
    public float stepSize
    {
        get { return h; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Step size must be positive");
            }
            h = value;
        }
    }

    /// <summary>
    /// Numerically integrates the <see cref="StateSpace.Derivatives(Matrix, Matrix)"/> function.
    /// </summary>
    /// <param name="ss"><see cref="StateSpace"/> to be integrated.</param>
    public abstract void Step(StateSpace ss);
}

/// <summary>
/// Contains <see cref="Integrator"/> functions.
/// </summary>
public static class Integrators
{
    /// <summary>
    /// Implements the forward Euler method.
    /// </summary>
    public class ForwardEuler : Integrator
    {
        public override void Step(StateSpace ss)
        {
            ss.states += h * ss.Derivatives(ss.states, ss.inputs);
        }
    }

    /// <summary>
    /// Implements Heun's method.
    /// </summary>
    public class Heun : Integrator
    {
        public override void Step(StateSpace ss)
        {
            Matrix k1 = ss.Derivatives(ss.states, ss.inputs);
            Matrix k2 = ss.Derivatives(ss.states + h * k1, ss.inputs);
            ss.states += (h / 2f) * (k1 + k2);
        }
    }

    /// <summary>
    /// Implements the 4th-order Runge-Kutta method.
    /// </summary>
    public class RK4 : Integrator
    {
        public override void Step(StateSpace ss)
        {
            Matrix k1 = h * ss.Derivatives(ss.states, ss.inputs);
            Matrix k2 = h * ss.Derivatives(ss.states + 0.5f * k1, ss.inputs);
            Matrix k3 = h * ss.Derivatives(ss.states + 0.5f * k2, ss.inputs);
            Matrix k4 = h * ss.Derivatives(ss.states + k3, ss.inputs);
            ss.states += (1f / 6f) * (k1 + 2f * k2 + 2f * k3 + k4);
        }
    }
}

/// <summary>
/// List of available <see cref="Integrator"/> methods
/// </summary>
public enum IntegrationMethod
{
    Euler, Heun, RK4
}