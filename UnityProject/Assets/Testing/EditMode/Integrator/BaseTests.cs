using NUnit.Framework;
using UnityEngine;

namespace IntegratorTests
{
    public struct TestConfig
    {
        public float StepSize;
        public float MaxSimulationTime;
        public bool Log;
        public float Tolerance;

        public TestConfig(float stepSize, float maxSimulationTime, bool log, float tolerance)
        {
            StepSize = stepSize;
            MaxSimulationTime = maxSimulationTime;
            Log = log;
            Tolerance = tolerance;
        }
    }

    public abstract class BaseTests<TIntegrator> where TIntegrator : Integrator, new()
    {
        protected TIntegrator TestIntegrator { get; set; }
        protected virtual TestConfig Config => new TestConfig();
        Helpers helpers = new();

        [Test]
        public void ExponentialGrowth()
        {
            var ss = helpers.CreateStateSpace
            (
                (states, inputs) => states,
                new Matrix(1, 1, new float[] { 1f }),
                TestIntegrator
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
                TestIntegrator
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
                TestIntegrator
            );
            helpers.RunSimulationTest(ss, Mathf.Sin, Config);
        }
    }
}
