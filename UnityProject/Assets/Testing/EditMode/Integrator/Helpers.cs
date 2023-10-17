using NUnit.Framework;
using System;
using UnityEngine;
using static StateSpace;

namespace IntegratorTests
{
    /// <summary>
    /// Provides utility functions and delegates for integration tests related to <see cref="StateSpace"/> simulations.
    /// </summary>
    public class Helpers
    {
        /// <summary>
        /// Creates a <see cref="StateSpace"/> instance with the specified parameters.
        /// </summary>
        /// <param name="derivativeFunction">The derivative function to be used in the StateSpace.</param>
        /// <param name="initialConditions">The initial conditions as a matrix.</param>
        /// <param name="integrator">The integrator to be used in the StateSpace.</param>
        /// <returns>A <see cref="StateSpace"/> instance.</returns>
        public StateSpace CreateStateSpace(DerivativeFunction derivativeFunction, Matrix initialConditions, Integrator integrator)
        {
            StateSpace ss = new StateSpace();
            ss.Initialize(initialConditions.RowCount, 0, initialConditions, derivativeFunction, integrator);
            return ss;
        }

        /// <summary>
        /// Delegate for solution functions used in simulations.
        /// </summary>
        /// <param name="time">The current time of the simulation.</param>
        /// <returns>The value of the solution function at the given time.</returns>
        public delegate float SolutionFunction(float time);

        /// <summary>
        /// Runs a simulation test for a given StateSpace, comparing the results against a solution function.
        /// </summary>
        /// <param name="ss">The StateSpace instance to be tested.</param>
        /// <param name="stepSize">The step size for the simulation.</param>
        /// <param name="maxSimulationTime">The maximum simulation time.</param>
        /// <param name="solution">The solution function to compare against.</param>
        /// <param name="log">Optional parameter to enable or disable logging.</param>
        /// <param name="tolerance">Optional parameter to specify the acceptable difference between the solution and the simulation.</param>
        public void RunSimulationTest
        (
            StateSpace ss,
            float maxSimulationTime,
            SolutionFunction solution,
            bool log = false,
            float tolerance = 5e-2f
        )
        {
            float currentTime = 0f;

            // Calculate the number of decimal places to round to based on the stepSize
            int decimalPlaces = Mathf.CeilToInt(-Mathf.Log10(ss.integrator.StepSize));

            while (currentTime < maxSimulationTime)
            {
                ss.Compute();
                currentTime = (float)Math.Round(currentTime + ss.integrator.StepSize, decimalPlaces);

                float expected = solution(currentTime);
                if (log)
                {
                    Debug.Log($"Using step size of {ss.integrator.StepSize}, at current time {currentTime}, Expected: {expected}, Actual: {ss.states[0, 0]}");
                }
                Assert.IsTrue(Mathf.Abs(expected - ss.states[0, 0]) < tolerance);
            }
        }
    }
}