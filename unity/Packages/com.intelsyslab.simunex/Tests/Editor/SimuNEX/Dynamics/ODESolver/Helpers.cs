using NUnit.Framework;
using SimuNEX.Dynamics;
using System;
using UnityEngine;
using static SimuNEX.Dynamics.StateSpace;

namespace ODESolverTests
{
    /// <summary>
    /// Provides utility functions and delegates for stepper tests related to <see cref="StateSpace"/> simulations.
    /// </summary>
    public class Helpers
    {
        /// <summary>
        /// Creates a <see cref="StateSpace"/> instance with the specified parameters.
        /// </summary>
        /// <param name="derivativeFunction">The derivative function to be used in the StateSpace.</param>
        /// <param name="initialConditions">The initial conditions as a matrix.</param>
        /// <returns>A <see cref="StateSpace"/> instance.</returns>
        public StateSpace CreateStateSpace(DerivativeFunction derivativeFunction, Matrix initialConditions)
        {
            StateSpace ss = new();
            ss.Initialize(initialConditions.RowCount, 0, initialConditions, derivativeFunction);
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
        /// <param name="solver">The solver to be used in the StateSpace.</param>
        /// <param name="solution">The solution function to compare against.</param>
        /// <param name="testConfig">Stepper configuration values for testing.</param>
        public void RunSimulationTest
        (
            StateSpace ss,
            ODESolver solver,
            SolutionFunction solution,
            TestConfig testConfig
        )
        {
            solver.stepSize = testConfig.StepSize;
            float currentTime = 0f;

            // Calculate the number of decimal places to round to based on the stepSize
            int decimalPlaces = Mathf.CeilToInt(-Mathf.Log10(solver.stepSize));

            while (currentTime < testConfig.MaxSimulationTime)
            {
                solver.Step(ss);
                currentTime = (float)Math.Round(currentTime + solver.stepSize, decimalPlaces);

                float expected = solution(currentTime);
                if (testConfig.Log)
                {
                    Debug.Log($"Using step size of {solver.stepSize}, at current time {currentTime}, " +
                        $"Expected: {expected}, Actual: {ss.states[0, 0]}");
                }

                Assert.IsTrue(Mathf.Abs(expected - ss.states[0, 0]) < testConfig.Tolerance);
            }
        }
    }
}
