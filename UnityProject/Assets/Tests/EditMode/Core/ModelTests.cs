using NUnit.Framework;
using SimuNEX;
using UnityEngine;

namespace CoreTests
{
    public class ModelTests
    {
        ExampleModel model;

        public class ExampleModel : Model
        {
            public ExampleModel()
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

            protected override ModelFunction modelFunction =>
                (IModelInput[] inputs, IModelOutput[] outputs) =>
                {
                    outputs[0].data = inputs[0].data;
                    outputs[1].data = (int)inputs[1].data == 1 ? 1 : 0;
                };
        }

        [SetUp]
        public void Setup()
        {
            // Assign
            GameObject gameObject = new();
            model = gameObject.AddComponent<ExampleModel>();
        }

        [Test]
        public void TestPorts()
        {
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

        [Test]
        public void TestModelFunction()
        {
            // Assign
            model.inports[0].data = 31f;
            model.inports[1].data = 1;

            Model.TestModel testModel = model.GetTestModel();

            // Act
            testModel.TestModelFunction();

            // Assert
            Assert.AreEqual(model.outports[0].data, 31f);
            Assert.AreEqual(model.outports[1].data, 1);

            // Second Run
            System.Random rand = new();
            model.inports[0].data = (float)rand.NextDouble();
            model.inports[1].data = rand.Next(1, 100);

            testModel.TestModelFunction();

            Assert.AreEqual(model.outports[0].data, model.inports[0].data);
            Assert.AreEqual(model.outports[1].data, 0);
        }
    }
}
