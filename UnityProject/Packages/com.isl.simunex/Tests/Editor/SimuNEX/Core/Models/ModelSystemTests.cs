using NUnit.Framework;
using SimuNEX;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimuNEXTests
{
    public class ModelSystemTests
    {
        public class ModelA : Model
        {
            public ModelA()
            {
                outputs = new List<ModelOutput> { new("outputA1"), new("outputA2") };

                inputs = new List<ModelInput> { new("inputA") };
            }

            protected override ModelFunction modelFunction
                => (ModelInput[] inputs, ModelOutput[] outputs) =>
                {
                    outputs[0].data[0] = inputs[0].data[0] * 2;
                    outputs[1].data[0] = inputs[0].data[0] + 2;
                };
        }

        public class ModelB : Model
        {
            public ModelB()
            {
                outputs = new List<ModelOutput> { new("outputB") };

                inputs = new List<ModelInput> { new("inputB") };
            }

            /// <summary>
            /// O = I + 10
            /// </summary>
            protected override ModelFunction modelFunction
                => (ModelInput[] inputs, ModelOutput[] outputs) => outputs[0].data[0] = inputs[0].data[0] + 10;
        }

        public class ModelC : Model
        {
            public ModelC()
            {
                outputs = new List<ModelOutput> { new("outputC") };

                inputs = new List<ModelInput> { new("inputC1"), new("inputC2") };
            }

            /// <summary>
            /// O = 5*I1 + 3*I2 + 2
            /// </summary>
            protected override ModelFunction modelFunction
                => (ModelInput[] inputs, ModelOutput[] outputs) =>
                outputs[0].data[0] = (5 * inputs[0].data[0]) + (3 * inputs[1].data[0]) + 2;
        }

        public class ModelD : Model
        {
            public ModelD()
            {
                outputs = new List<ModelOutput> { new("outputD") };

                inputs = new List<ModelInput> { new("inputD") };
            }

            /// <summary>
            /// O = 1 if I > 10 else 0
            /// </summary>
            protected override ModelFunction modelFunction
                => (ModelInput[] inputs, ModelOutput[] outputs) => outputs[0].data[0] = inputs[0].data[0] > 10? 1 : 0;
        }

        public class ModelE : Model
        {
            public ModelE()
            {
                outputs = new List<ModelOutput> { new("outputE") };

                inputs = new List<ModelInput> { new("inputE1"), new("inputE2") };
            }

            /// <summary>
            /// O = I1*I2
            /// </summary>
            protected override ModelFunction modelFunction
                => (ModelInput[] inputs, ModelOutput[] outputs) => outputs[0].data[0] = inputs[0].data[0] * inputs[1].data[0];
        }

        private ModelSystem modelSystem;
        private ModelA modelA;
        private ModelB modelB;
        private ModelC modelC;
        private ModelD modelD;
        private ModelE modelE;

        [SetUp]
        public void Setup()
        {
            // Assign
            GameObject gameObject = new();
            modelSystem = gameObject.AddComponent<ModelSystem>();

            modelA = gameObject.AddComponent<ModelA>();
            modelB = gameObject.AddComponent<ModelB>();
            modelC = gameObject.AddComponent<ModelC>();
            modelD = gameObject.AddComponent<ModelD>();
            modelE = gameObject.AddComponent<ModelE>();

            // Order must be A -> B -> D -> E -> C
            modelSystem.models.AddRange(new Model[] { modelA, modelB, modelD, modelE, modelC });
        }

        [Test]
        public void TestMapOutput()
        {
            // Arrange
            ModelOutput systemOutputA = new("systemOutputA");
            ModelOutput systemOutputB = new("systemOutputB");
            modelSystem.AddPorts(systemOutputA, systemOutputB);

            // Act
            modelSystem.MapOutput(modelA.outports[0], systemOutputA);
            modelSystem.MapOutput(modelB.outports[0], systemOutputB);
            modelSystem.Link();

            modelA.inports[0].data[0] = 5f;
            modelB.inports[0].data[0] = 3f;
            modelSystem.Step();

            // Assert
            Assert.AreEqual(10f, modelA.outports[0].data[0]);
            Assert.AreEqual(13f, modelB.outports[0].data[0]);

            Assert.AreEqual(10f, systemOutputA.data[0]);
            Assert.AreEqual(13f, systemOutputB.data[0]);
        }

        [Test]
        public void TestUnmapOutput()
        {
            // Arrange
            ModelOutput systemOutputA = new("systemOutputA");
            modelSystem.AddPorts(systemOutputA);

            modelSystem.MapOutput(modelA.outports[0], systemOutputA);
            modelSystem.Link();

            modelA.inports[0].data[0] = 5f;
            modelSystem.Step();
            Assert.AreEqual(10f, systemOutputA.data[0]);

            // Act
            modelSystem.UnmapOutput(modelA.outports[0]);

            modelA.inports[0].data[0] = 7f;
            modelSystem.Step();

            // Assert
            Assert.AreNotEqual(14f, systemOutputA.data[0]); // The output should not be updated anymore
        }

        [Test]
        public void TestInvalidOutputMapping()
        {
            // Arrange
            ModelOutput systemOutputA = new("systemOutputA");
            modelSystem.AddPorts(systemOutputA);

            modelSystem.MapOutput(modelA.outports[0], systemOutputA);

            // Act & Assert
            _ = Assert.Throws<InvalidOperationException>(
                () => modelSystem.MapOutput(modelB.outports[0], systemOutputA),
                "This system output is already mapped to another model output.");
        }

        [Test]
        public void TestInvalidOutputPortSizes()
        {
            // Arrange
            ModelOutput systemOutputA = new("systemOutputA", 2); // Different size
            modelSystem.AddPorts(systemOutputA);

            // Act & Assert
            _ = Assert.Throws<InvalidOperationException>(
                () => modelSystem.MapOutput(modelA.outports[0], systemOutputA),
                "The output sizes do not match.");
        }

        [Test]
        public void TestInvalidOutputPortMapping()
        {
            // Arrange
            ModelOutput systemOutputA = new("systemOutputA", 1);
            ModelOutput invalidOutput = new("invalidOutput", 1);

            modelSystem.AddPorts(systemOutputA);

            // Act & Assert
            _ = Assert.Throws<InvalidOperationException>(
                () => modelSystem.MapOutput(invalidOutput, systemOutputA),
                "The model output does not belong to this ModelSystem.");

            ModelOutput systemOutputB = new("systemOutputB", 1);

            // Act & Assert
            _ = Assert.Throws<InvalidOperationException>(
                () => modelSystem.MapOutput(modelA.outports[0], systemOutputB),
                "The system output does not belong to this ModelSystem.");
        }

        [Test]
        public void TestInvalidOutputUnmap()
        {
            // Arrange
            ModelOutput invalidOutput = new("invalidOutput", 1);

            // Act & Assert
            _ = Assert.Throws<InvalidOperationException>(
                () => modelSystem.UnmapOutput(invalidOutput),
                "The model output does not belong to this ModelSystem.");
        }

        [Test]
        public void TestMapInput()
        {
            // Arrange
            ModelInput systemInputA = new("systemInputA", 1);
            modelSystem.AddPorts(systemInputA);

            // Act
            modelSystem.MapInput(systemInputA, modelA.inports[0], modelB.inports[0]);
            modelSystem.Link();

            systemInputA.data[0] = 5f;
            modelSystem.Step();

            // Assert
            Assert.AreEqual(5f, modelA.inports[0].data[0]);
            Assert.AreEqual(5f, modelB.inports[0].data[0]);
        }

        [Test]
        public void TestUnmapInput()
        {
            // Arrange
            ModelInput systemInputA = new("systemInputA", 1);
            modelSystem.AddPorts(systemInputA);

            modelSystem.MapInput(systemInputA, modelA.inports[0]);
            modelSystem.Link();
            systemInputA.data[0] = 5f;
            modelSystem.Step();
            Assert.AreEqual(5f, modelA.inports[0].data[0]);

            // Act
            modelSystem.UnmapInput(systemInputA);

            systemInputA.data[0] = 7f;
            modelSystem.Step();

            // Assert
            Assert.AreNotEqual(7f, modelA.inports[0].data[0]); // The input should not be updated anymore
        }

        [Test]
        public void TestInvalidInputMapping()
        {
            // Arrange
            ModelInput systemInputA = new("systemInputA", 1);
            modelSystem.AddPorts(systemInputA);

            modelSystem.MapInput(systemInputA, modelA.inports[0]);

            // Act & Assert
            _ = Assert.Throws<InvalidOperationException>(
                () => modelSystem.MapInput(systemInputA, modelA.inports[0]),
                "This model input is already mapped to another system input.");
        }

        [Test]
        public void TestInvalidInputPortSizes()
        {
            // Arrange
            ModelInput systemInputA = new("systemInputA", 2); // Different size
            modelSystem.AddPorts(systemInputA);

            // Act & Assert
            _ = Assert.Throws<InvalidOperationException>(
                () => modelSystem.MapInput(systemInputA, modelA.inports[0]),
                "The input sizes do not match.");
        }

        [Test]
        public void TestInvalidInputPortMapping()
        {
            // Arrange
            ModelInput systemInputA = new("systemInputA", 1);
            ModelInput invalidInput = new("invalidInput", 1);

            modelSystem.AddPorts(systemInputA);

            // Act & Assert
            _ = Assert.Throws<InvalidOperationException>(
                () => modelSystem.MapInput(systemInputA, invalidInput),
                "The model input does not belong to any Model within this ModelSystem.");

            ModelInput systemInputB = new("systemInputB", 1);

            // Act & Assert
            _ = Assert.Throws<InvalidOperationException>(
                () => modelSystem.MapInput(systemInputB, modelA.inports[0]),
                "The system input does not belong to this ModelSystem.");
        }

        [Test]
        public void TestInvalidUnmapInput()
        {
            // Arrange
            ModelInput invalidInput = new("invalidInput", 1);

            // Act & Assert
            _ = Assert.Throws<InvalidOperationException>(
                () => modelSystem.UnmapInput(invalidInput),
                "The system input does not belong to this ModelSystem.");
        }

        [Test]
        public void TestMapInternal()
        {
            // Arrange
            ModelInput systemInputA = new("systemInputA", 1);
            ModelOutput systemOutputA = new("systemOutputA", 1);
            modelSystem.AddPorts(systemInputA, systemOutputA);

            ModelOutput modelOutputA = modelA.outports[0];

            // Act
            modelSystem.MapInput(systemInputA, modelA.inports[0]);  // M -> A
            modelSystem.MapInternal(modelOutputA, modelB.inports[0]); // M -> A -> B
            modelSystem.MapOutput(modelB.outports[0], systemOutputA); // M -> A -> B -> M
            modelSystem.Link();

            systemInputA.data[0] = 5f;
            modelSystem.Step();

            // Assert
            Assert.AreEqual(10f, modelA.outports[0].data[0]);
            Assert.AreEqual(10f, modelB.inports[0].data[0]);
            Assert.AreEqual(20f, systemOutputA.data[0]);
        }

        [Test]
        public void TestInvalidInternalMapping()
        {
            // Arrange
            ModelOutput modelOutputA = modelA.outports[0];
            ModelInput invalidInput = new("invalidInput", 1);

            // Act & Assert
            _ = Assert.Throws<InvalidOperationException>(
                () => modelSystem.MapInternal(modelOutputA, invalidInput),
                "The model input does not belong to any Model within this ModelSystem.");
        }

        [Test]
        public void TestCompleteModel()
        {
            // Arrange
            ModelInput systemInputA = new("systemInputA", 1);
            ModelInput systemInputB = new("systemInputB", 1);

            ModelOutput systemOutputC = new("systemOutputC", 1);
            ModelOutput systemOutputE = new("systemOutputE", 1);

            modelSystem.AddPorts(systemInputA, systemInputB, systemOutputC, systemOutputE);

            // Act
            modelSystem.MapInput(systemInputA, modelA.inports[0]); // M1 -> A
            modelSystem.MapInput(systemInputB, modelB.inports[0]); // M2 -> B

            modelSystem.MapInternal(modelA.outports[0], modelC.inports[0]); // M1 -> A1 -> C1
            modelSystem.MapInternal(modelA.outports[1], modelD.inports[0]); // M1 -> A2 -> D

            modelSystem.MapInternal(modelB.outports[0], modelE.inports[0]); // M2 -> B1 -> E1
            modelSystem.MapInternal(modelD.outports[0], modelE.inports[1]); // M1 -> A2 -> D1 -> E1

            modelSystem.MapInternal(modelE.outports[0], modelC.inports[1]); // M1 -> A2 -> D1 -> E1 -> C2
            modelSystem.Link();

            systemInputA.data[0] = 15f;
            systemInputB.data[0] = 25f;

            modelSystem.Step();

            // A = 2*M[0], 2+M[0]
            Assert.AreEqual(30f, modelA.outports[0].data[0]);
            Assert.AreEqual(17f, modelA.outports[1].data[0]);

            // B = M[1] + 10
            Assert.AreEqual(35f, modelB.outports[0].data[0]);

            // C = 5*A[0] + 3*E[0] + 2
            Assert.AreEqual(150f + 105f + 2f, modelC.outports[0].data[0]);

            // D = 1 if A[1] > 10 else 0
            Assert.AreEqual(1f, modelD.outports[0].data[0]);

            // E = B[0]*D[0]
            Assert.AreEqual(35f, modelE.outports[0].data[0]);
        }
    }
}
