using NUnit.Framework;
using SimuNEX;
using UnityEngine;

namespace CoreTests
{
    public class SummerTests
    {
        Summer summer;

        [SetUp]
        public void Setup()
        {
            // Assign
            GameObject gameObject = new();
            summer = gameObject.AddComponent<Summer>();

            ModelInput[] inputs = new ModelInput[]
            {
                new("input1"),
                new("input2"),
                new("input3")
            };

            summer.Add(inputs);
        }

        [Test]
        public void TestPorts()
        {
            // Act
            ModelOutput[] outports = summer.outports;
            ModelInput[] inports = summer.inports;

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
            summer.inports[0].data[0] = 7f;
            summer.inports[1].data[0] = 1f;
            summer.inports[2].data[0] = 2f;

            Model.TestModel testModel = summer.GetTestModel();

            // Act
            testModel.TestModelFunction();

            // Assert
            Assert.AreEqual(summer.outports[0].data[0], 10f);
        }
    }
}
