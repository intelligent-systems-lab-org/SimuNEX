using FactoryTests;
using NUnit.Framework;
using SimuNEX.Models;
using SimuNEX.Solvers;
using System;
using UnityEngine;

namespace ODESolverTests
{
    /// <summary>
    /// Stepper configuration values for testing.
    /// </summary>
    public class TestConfig
    {
        /// <summary>
        /// Step size for the stepper method.
        /// </summary>
        public float StepSize;

        /// <summary>
        /// The maximum time to run the stepper method for.
        /// </summary>
        public float MaxSimulationTime;

        /// <summary>
        /// Enables debug messages.
        /// </summary>
        public bool Log;

        /// <summary>
        /// Tolerance value to be used for comparison.
        /// </summary>
        public float Tolerance;

        public TestConfig(float stepSize = 0.02f, float maxSimulationTime = 1f, bool log = false, float tolerance = 5e-2f)
        {
            StepSize = stepSize;
            MaxSimulationTime = maxSimulationTime;
            Log = log;
            Tolerance = tolerance;
        }
    }

    /// <summary>
    /// Tests the methods specific to the base class.
    /// </summary>
    public class BaseTests
    {
        private class TestSolver : ODESolver
        {
            public override void Step(StateSpace ss)
            {
            }
        }

        [Test]
        public void StepSize_WhenSetToPositiveValue_ShouldAssignValue()
        {
            TestSolver solver = new();
            const float expected = 0.03f; // Choose a valid positive value that meets all criteria

            solver.stepSize = expected;

            Assert.AreEqual(expected, solver.stepSize);
        }

        [Test]
        public void StepSize_WhenSetToNegative_ThrowsArgumentException()
        {
            TestSolver solver = new();

            _ = Assert.Throws<ArgumentException>(() => solver.stepSize = -1f);
        }

        [Test]
        public void StepSize_WhenSetToZero_ThrowsArgumentException()
        {
            TestSolver solver = new();

            _ = Assert.Throws<ArgumentException>(() => solver.stepSize = 0f);
        }

        [Test]
        public void StepSize_WhenSetLessThanMinimum_ThrowsArgumentException()
        {
            TestSolver solver = new();

            _ = Assert.Throws<ArgumentException>(() => solver.stepSize = 0.001f);
        }

        [Test]
        public void StepSize_WhenSetToNonMultipleOf005_ThrowsArgumentException()
        {
            TestSolver solver = new();

            _ = Assert.Throws<ArgumentException>(() => solver.stepSize = 0.027f);
        }
    }

    public class SolverTestsSetup : TypeRegisterSetup<ODESolver>
    {
    }

    /// <summary>
    /// Tests each <see cref="ODESolver"/> solver capability.
    /// </summary>
    /// <typeparam name="TSolver">A <see cref="ODESolver"/> to test.</typeparam>
    public abstract class SolverTests<TSolver> where TSolver : ODESolver, new()
    {
        protected TSolver solverInstance { get; set; }
        protected virtual TestConfig Config => new();

        private readonly Helpers helpers = new();

        [SetUp]
        public void SetUp()
        {
            solverInstance = new TSolver();

            // Register the solver type as being tested
            SolverTestsSetup.RegisterTest<TSolver>();
        }

        [Test]
        public void ExponentialGrowth()
        {
            StateSpace ss = helpers.CreateStateSpace
            (
                (states, inputs) => states,
                new Matrix(1, 1, new float[] { 1f }),
                solverInstance
            );
            helpers.RunSimulationTest(ss, Mathf.Exp, Config);
        }

        [Test]
        public void LinearGrowth()
        {
            StateSpace ss = helpers.CreateStateSpace
            (
                (states, inputs) => new Matrix(1, 1, new float[] { 5f }),
                new Matrix(1, 1, new float[] { 0f }),
                solverInstance
            );
            helpers.RunSimulationTest(ss, (float time) => 5f * time, Config);
        }

        [Test]
        public void HarmonicOscillator()
        {
            StateSpace ss = helpers.CreateStateSpace
            (
                (states, inputs) =>
                {
                    float y1 = states[0, 0];
                    float y2 = states[1, 0];
                    return new Matrix(2, 1, new float[] { y2, -y1 });
                },
                new Matrix(2, 1, new float[] { 0f, 1f }),
                solverInstance
            );
            helpers.RunSimulationTest(ss, Mathf.Sin, Config);
        }
    }
}
