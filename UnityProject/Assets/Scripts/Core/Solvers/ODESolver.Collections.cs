using SimuNEX.Models;

namespace SimuNEX.Solvers
{
    /// <summary>
    /// Implements the forward Euler method.
    /// </summary>
    public class ForwardEuler : ODESolver
    {
        public override void Step(StateSpace ss)
        {
            ss.states += stepSize * ss.Derivatives(ss.states, ss.inputs);
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
            Matrix k2 = ss.Derivatives(ss.states + (stepSize * k1), ss.inputs);
            ss.states += stepSize / 2f * (k1 + k2);
        }
    }

    /// <summary>
    /// Implements the 4th-order Runge-Kutta method.
    /// </summary>
    public class RK4 : ODESolver
    {
        public override void Step(StateSpace ss)
        {
            Matrix k1 = stepSize * ss.Derivatives(ss.states, ss.inputs);
            Matrix k2 = stepSize * ss.Derivatives(ss.states + (0.5f * k1), ss.inputs);
            Matrix k3 = stepSize * ss.Derivatives(ss.states + (0.5f * k2), ss.inputs);
            Matrix k4 = stepSize * ss.Derivatives(ss.states + k3, ss.inputs);
            ss.states += 1f / 6f * (k1 + (2f * k2) + (2f * k3) + k4);
        }
    }
}
