using NUnit.Framework;
using SimuNEX;
using UnityEngine;

namespace CoreTests
{
    public class AdderTests
    {
        private Adder adder;

        [SetUp]
        public void Setup()
        {
            // Assign
            GameObject gameObject = new();
            adder = gameObject.AddComponent<Adder>();

            adder.Create
            (
                new string[]
                {
                    "input1", "input2", "input3"
                }
            );
        }

        [Test]
        public void TestPorts()
        {
            // Act
            ModelOutput[] outports = adder.outports;
            ModelInput[] inports = adder.inports;

            // Assert
            // Verify number of added outputs
            Assert.AreEqual(1, outports.Length);

            // Verify number of added inputs
            Assert.AreEqual(3, inports.Length);
        }

        [Test]
        public void TestModelFunction()
        {
            // Assign
            adder.inports[0].data[0] = 7f;
            adder.inports[1].data[0] = 1f;
            adder.inports[2].data[0] = 2f;

            // Act
            adder.Step();

            // Assert
            Assert.AreEqual(adder.outports[0].data[0], 10f);
        }
    }
}
