using SimuNEX;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CoreTests
{
    public class ModelTests
    {
        public class TestModel : Model
        {
            public override IModelPort[] ports => new IModelPort[]
            {
                new ModelOutput<float>("floatO"),
                new ModelOutput<int>("intO"),
                new ModelOutput<Vector3>("Vector3O"),
                new ModelOutput<Quaternion>("QuaternionO"),
                new ModelInput<float>("floatI"),
                new ModelInput<int>("intI")
            };

            public override IBehavioral behavorial => null;
        }

        [Test]
        public void TestPorts()
        {
            // Assign
            GameObject gameObject = new();
            TestModel model = gameObject.AddComponent<TestModel>();
            model.Init();

            // Act
            List<IModelOutput> outports = model.outports.ToList();
            List<IModelInput> inports = model.inports.ToList();

            // Assert
            // Verify number of added outputs
            Assert.AreEqual(4, outports.Count);

            // Verify number of added inputs
            Assert.AreEqual(2, inports.Count);

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
