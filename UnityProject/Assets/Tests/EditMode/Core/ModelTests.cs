using NUnit.Framework;
using SimuNEX;
using UnityEngine;

namespace CoreTests
{
    public class ModelTests
    {
        public class TestModel : Model
        {
            public TestModel()
            {
                outputs = new
                (
                    new IModelOutput[]
                    {
                        new ModelOutput<float>("floatO"),
                        new ModelOutput<int>("intO"),
                        new ModelOutput<Vector3>("Vector3O"),
                        new ModelOutput<Quaternion>("QuaternionO")
                    }
                );

                inputs = new
                (
                    new IModelInput[]
                    {
                        new ModelInput<float>("floatI"),
                        new ModelInput<int>("intI")
                    }
                );
            }

            protected override ModelFunction modelFunction => throw new System.NotImplementedException();
        }

        [Test]
        public void TestPorts()
        {
            // Assign
            GameObject gameObject = new();
            TestModel model = gameObject.AddComponent<TestModel>();

            // Act
            IModelOutput[] outports = model.outports;
            IModelInput[] inports = model.inports;

            // Assert
            // Verify number of added outputs
            Assert.AreEqual(4, outports.Length);

            // Verify number of added inputs
            Assert.AreEqual(2, inports.Length);

            // Verify ports
            Assert.IsTrue(outports[0] is ModelOutput<float>);
            Assert.IsTrue(outports[1] is ModelOutput<int>);
            Assert.IsTrue(outports[2] is ModelOutput<Vector3>);
            Assert.IsTrue(outports[3] is ModelOutput<Quaternion>);

            Assert.IsTrue(inports[0] is ModelInput<float>);
            Assert.IsTrue(inports[1] is ModelInput<int>);
        }
    }
}
