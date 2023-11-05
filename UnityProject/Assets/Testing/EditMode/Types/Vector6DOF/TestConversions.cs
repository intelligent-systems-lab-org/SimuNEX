using NUnit.Framework;
using SimuNEX;
using System;
using UnityEngine;

namespace Vector6DOFTests
{
    public class TestConversions
    {
        [Test]
        public void ImplicitOperator_ConvertsToArray()
        {
            // Arrange  

            // Act
            float[] result = (Vector6DOF)(new()
            {
                linear = new Vector3(1f, 2f, 3f),
                angular = new Vector3(4f, 5f, 6f)
            });

            // Assert
            Assert.AreEqual(1f, result[0]);
            Assert.AreEqual(2f, result[1]);
            Assert.AreEqual(3f, result[2]);
            Assert.AreEqual(4f, result[3]);
            Assert.AreEqual(5f, result[4]);
            Assert.AreEqual(6f, result[5]);
        }

        [Test]
        public void ImplicitOperator_ConvertsToMatrix()
        {
            // Arrange  

            // Act
            Matrix result = (Vector6DOF)(new()
            {
                linear = new Vector3(1f, 2f, 3f),
                angular = new Vector3(4f, 5f, 6f)
            });

            // Assert
            Assert.AreEqual(1f, result[0, 0]);
            Assert.AreEqual(2f, result[1, 0]);
            Assert.AreEqual(3f, result[2, 0]);
            Assert.AreEqual(4f, result[3, 0]);
            Assert.AreEqual(5f, result[4, 0]);
            Assert.AreEqual(6f, result[5, 0]);
        }

        [Test]
        public void ToString_ReturnsStringRepresentation()
        {
            // Arrange
            float[] v = new float[] { 1f, 2f, 3f, 4f, 5f, 6f };

            Vector6DOF vector6DOF = new(v);

            // Act
            string result = vector6DOF.ToString();

            // Assert
            const string expected = "(1 2 3 4 5 6)";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestStringToVector6DOFConversion()
        {
            Vector6DOF v = "[1; 2; 3; 4; 5; 6]";

            Assert.AreEqual(1, v.linear.x);
            Assert.AreEqual(2, v.linear.y);
            Assert.AreEqual(3, v.linear.z);
            Assert.AreEqual(4, v.angular.x);
            Assert.AreEqual(5, v.angular.y);
            Assert.AreEqual(6, v.angular.z);

            // Invalid: Missing one element
            const string vectorString2 = "[1; 2; 3; 4; 5]";

            InvalidOperationException exception = Assert
                .Throws<InvalidOperationException>(() => { Vector6DOF _ = vectorString2; });
            Assert.AreEqual("Invalid vector string. Expected 6 elements.", exception.Message);
        }

        [Test]
        public void TestMatrixToVector6DOFImplicitConversion_1x6()
        {
            // Arrange
            Matrix matrix = new(1, 6);
            matrix[0, 0] = 1;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;
            matrix[0, 3] = 4;
            matrix[0, 4] = 5;
            matrix[0, 5] = 6;

            // Act
            Vector6DOF result = matrix;

            // Assert
            Assert.AreEqual(new Vector3(1, 2, 3), result.linear);
            Assert.AreEqual(new Vector3(4, 5, 6), result.angular);
        }

        [Test]
        public void TestMatrixToVector6DOFImplicitConversion_6x1()
        {
            // Arrange
            Matrix matrix = new(6, 1);
            matrix[0, 0] = 1;
            matrix[1, 0] = 2;
            matrix[2, 0] = 3;
            matrix[3, 0] = 4;
            matrix[4, 0] = 5;
            matrix[5, 0] = 6;

            // Act
            Vector6DOF result = matrix;

            // Assert
            Assert.AreEqual(new Vector3(1, 2, 3), result.linear);
            Assert.AreEqual(new Vector3(4, 5, 6), result.angular);
        }

        [Test]
        public void TestMatrixToVector6DOFInvalidConversion()
        {
            // Arrange
            // Invalid size for conversion
            Matrix matrix = new(2, 3);

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () =>
                {
                    Vector6DOF result = matrix;
                },
                "Matrix must be 1x6 or 6x1 to convert to Vector6DOF.");
        }
    }
}
