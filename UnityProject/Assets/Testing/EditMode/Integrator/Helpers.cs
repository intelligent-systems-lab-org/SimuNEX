using NUnit.Framework;
using System;
using UnityEngine;
using static StateSpace;

namespace IntegratorTests
{
    public class Helpers
    {
        public StateSpace CreateStateSpace(DerivativeFunction derivativeFunction, Matrix initialConditions, Integrator integrator)
        {
            StateSpace ss = new StateSpace();
            ss.Initialize(initialConditions.RowCount, 0, initialConditions, derivativeFunction, integrator);
            return ss;
        }

        public delegate float SolutionFunction(float time);

        public void RunSimulationTest
        (
            StateSpace ss,
            float stepSize,
            float maxSimulationTime,
            SolutionFunction solution,
            bool log = false,
            float tolerance = 5e-2f
        )
        {
            float currentTime = 0f;

            // Calculate the number of decimal places to round to based on the stepSize
            int decimalPlaces = Mathf.CeilToInt(-Mathf.Log10(stepSize));

            while (currentTime < maxSimulationTime)
            {
                ss.Compute();
                currentTime = (float)Math.Round(currentTime + stepSize, decimalPlaces);

                float expected = solution(currentTime);
                if (log)
                {
                    Debug.Log($"Using step size of {stepSize}, at current time {currentTime}, Expected: {expected}, Actual: {ss.states[0, 0]}");
                }
                Assert.IsTrue(Mathf.Abs(expected - ss.states[0, 0]) < tolerance);
            }
        }
    }
}