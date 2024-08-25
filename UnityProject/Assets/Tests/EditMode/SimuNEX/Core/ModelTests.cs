using NUnit.Framework;
using SimuNEX;
using UnityEngine;

namespace SimuNEXTests
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
                    new ModelOutput[]
                    {
                        new("floatOO"),
                        new("floatO1"),
                        new("floatO2"),
                        new("floatO3")
                    }
                );

                inputs = new
                (
                    new ModelInput[]
                    {
                        new("floatI1"),
                        new("intI2")
                    }
                );
            }

            protected override ModelFunction modelFunction =>
                (ModelInput[] inputs, ModelOutput[] outputs) =>
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
            ModelOutput[] outports = model.outports;
            ModelInput[] inports = model.inports;

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

            // Act
            model.Step();

            // Assert
            Assert.AreEqual(model.outports[0].data[0], 31f);
            Assert.AreEqual(model.outports[1].data[0], 1);

            // Second Run
            System.Random rand = new();
            model.inports[0].data[0] = (float)rand.NextDouble();
            model.inports[1].data[0] = rand.Next(1, 100);

            model.Step();

            Assert.AreEqual(model.outports[0].data[0], model.inports[0].data[0]);
            Assert.AreEqual(model.outports[1].data[0], 0);
        }
    }
}
