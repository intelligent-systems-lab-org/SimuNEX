using NUnit.Framework;
using UnityEngine;
using SimuNEX;
using SolverTests;

namespace StepperTests
{
    public class TestConfig
    {
        public float StepSize;
        public float MaxSimulationTime;
        public bool Log;
        public float Tolerance;

        public TestConfig(float stepSize = 0.02f, float maxSimulationTime = 1f, bool log = false, float tolerance = 5e-2f)
        {
            StepSize = stepSize;
            MaxSimulationTime = maxSimulationTime;
            Log = log;
            Tolerance = tolerance;
        }
    }

    public abstract class BaseTests<TStepper> where TStepper : ODESolver, new()
    {
        protected TStepper TestStepper { get; set; }
        protected virtual TestConfig Config => new();
        Helpers helpers = new();

        [Test]
        public void ExponentialGrowth()
        {
            var ss = helpers.CreateStateSpace
            (
                (states, inputs) => states,
                new Matrix(1, 1, new float[] { 1f }),
                TestStepper
            );
            helpers.RunSimulationTest(ss, Mathf.Exp, Config);
        }

        [Test]
        public void LinearGrowth()
        {
            var ss = helpers.CreateStateSpace
            (
                (states, inputs) => new Matrix(1, 1, new float[] { 5f }),
                new Matrix(1, 1, new float[] { 0f }),
                TestStepper
            );
            helpers.RunSimulationTest(ss, (float time) => 5f * time, Config);
        }

        [Test]
        public void HarmonicOscillator()
        {
            var ss = helpers.CreateStateSpace
            (
                (states, inputs) =>
                {
                    var y1 = states[0, 0];
                    var y2 = states[1, 0];
                    return new Matrix(2, 1, new float[] { y2, -y1 });
                },
                new Matrix(2, 1, new float[] { 0f, 1f }),
                TestStepper
            );
            helpers.RunSimulationTest(ss, Mathf.Sin, Config);
        }
    }
}
