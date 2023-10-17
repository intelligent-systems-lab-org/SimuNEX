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
            var ss = helpers.CreateStateSpace
            (
                (states, inputs) => states,
                new Matrix(1, 1, new float[] { 1f }),
                new Integrators.ForwardEuler()
            );
            helpers.RunSimulationTest(ss, 1f, Mathf.Exp);
        }

        [Test]
        public void LinearGrowth()
        {
            var ss = helpers.CreateStateSpace
            (
                (states, inputs) => new Matrix(1, 1, new float[] { 5f }),
                new Matrix(1, 1, new float[] { 0f }),
                new Integrators.ForwardEuler()
            );
            helpers.RunSimulationTest(ss, 1f, (float time) => 5f * time);
        }

        [Test]
        public void SineWave()
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
                new Integrators.ForwardEuler()
            );

            helpers.RunSimulationTest(ss, 1f, Mathf.Sin);
        }
    }
}