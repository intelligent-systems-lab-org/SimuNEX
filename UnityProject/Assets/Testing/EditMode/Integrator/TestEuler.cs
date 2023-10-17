using NUnit.Framework;
using UnityEngine;

namespace IntegratorTests
{
    public class TestEuler
    {
        Helpers helpers = new();

        [Test]
        public void ExponentialGrowth()
        {
            var ss = helpers.CreateStateSpace((states, inputs) => states, new Matrix(1, 1, new float[] { 1f }), new Integrators.ForwardEuler());
            ss.integrator.StepSize = Time.fixedDeltaTime;

            helpers.RunSimulationTest(ss, Time.fixedDeltaTime, 1f, Mathf.Exp);
        }
    }
}