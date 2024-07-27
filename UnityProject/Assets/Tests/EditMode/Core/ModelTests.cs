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
                        new ModelOutput("floatOO"),
                        new ModelOutput("floatO1"),
                        new ModelOutput("floatO2"),
                        new ModelOutput("floatO3")
                    }
                );

                inputs = new
                (
                    new IModelInput[]
                    {
                        new ModelInput("floatI1"),
                        new ModelInput("intI2")
                    }
                );
            }

            protected override ModelFunction modelFunction =>
                (IModelInput[] inputs, IModelOutput[] outputs) =>
                {
                    outputs[0].data = inputs[0].data;
                    outputs[1].data[0] = (int)inputs[1].data[0] == 1 ? 1 : 0;
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
        }

        [Test]
        public void TestModelFunction()
        {
            // Assign
            model.inports[0].data[0] = 31f;
            model.inports[1].data[0] = 1;

            Model.TestModel testModel = model.GetTestModel();

            // Act
            testModel.TestModelFunction();

            // Assert
            Assert.AreEqual(model.outports[0].data[0], 31f);
            Assert.AreEqual(model.outports[1].data[0], 1);

            // Second Run
            System.Random rand = new();
            model.inports[0].data[0] = (float)rand.NextDouble();
            model.inports[1].data[0] = rand.Next(1, 100);

            testModel.TestModelFunction();

            Assert.AreEqual(model.outports[0].data[0], model.inports[0].data[0]);
            Assert.AreEqual(model.outports[1].data[0], 0);
        }
    }
}
