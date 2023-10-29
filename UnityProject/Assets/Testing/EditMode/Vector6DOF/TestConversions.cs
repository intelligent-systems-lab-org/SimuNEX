using NUnit.Framework;
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
            Vector6DOF v = new()
            {
                linear = new Vector3(1f, 2f, 3f),
                angular = new Vector3(4f, 5f, 6f)
            };

            // Act
            float[] result = v;

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
            Vector6DOF v = new()
            {
                linear = new Vector3(1f, 2f, 3f),
                angular = new Vector3(4f, 5f, 6f)
            };

            // Act
            Matrix result = v;

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
            string expected = "(1 2 3 4 5 6)";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestStringToVector6DOFConversion()
        {
            string vectorString = "[1; 2; 3; 4; 5; 6]";

            Vector6DOF v = vectorString;

            Assert.AreEqual(1, v.linear.x);
            Assert.AreEqual(2, v.linear.y);
            Assert.AreEqual(3, v.linear.z);
            Assert.AreEqual(4, v.angular.x);
            Assert.AreEqual(5, v.angular.y);
            Assert.AreEqual(6, v.angular.z);

            // Invalid: Missing one element
            string vectorString2 = "[1; 2; 3; 4; 5]";

            var exception = Assert
                .Throws<InvalidOperationException>(() => { Vector6DOF _ = vectorString2; });
            Assert.AreEqual("Invalid vector string. Expected 6 elements.", exception.Message);
        }
    }
}