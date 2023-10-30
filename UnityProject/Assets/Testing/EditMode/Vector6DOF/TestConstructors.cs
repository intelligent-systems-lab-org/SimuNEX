using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vector6DOFTests
{
    public class TestConstructors
    {
        [Test]
        public void Constructor_Default_ShouldInitializeWithZeroValues()
        {
            // Act
            Vector6DOF v = new();

            // Assert
            Assert.AreEqual(Vector3.zero, v.linear);
            Assert.AreEqual(Vector3.zero, v.angular);
        }

        [Test]
        public void Constructor_WithLinearAndAngular_VectorsAssignedCorrectly()
        {
            // Arrange
            Vector3 linear = new(1f, 2f, 3f);
            Vector3 angular = new(4f, 5f, 6f);

            // Act
            Vector6DOF v = new(linear, angular);

            // Assert
            Assert.AreEqual(linear, v.linear);
            Assert.AreEqual(angular, v.angular);
        }

        [Test]
        public void Constructor_WithArray_AssignsLinearAndAngularCorrectly()
        {
            // Arrange
            float[] v = new float[] { 1f, 2f, 3f, 4f, 5f, 6f };

            // Act
            Vector6DOF result = new(v);

            // Assert
            Assert.AreEqual(new Vector3(1f, 2f, 3f), result.linear);
            Assert.AreEqual(new Vector3(4f, 5f, 6f), result.angular);
        }

        [Test]
        public void TestVector6DOFConstructorWithString()
        {
            string vectorString = "[1; 2; 3; 4; 5; 6]";

            Vector6DOF v = new(vectorString);

            Assert.AreEqual(new Vector3(1, 2, 3), v.linear);
            Assert.AreEqual(new Vector3(4, 5, 6), v.angular);
        }

        [Test]
        public void Vector6DOF_Constructor_InitializesComponents()
        {
            float u = 1.5f;
            float v = -0.5f;
            float w = 2.0f;
            float p = 0.1f;
            float q = -0.3f;
            float r = 0.5f;

            Vector6DOF vector6DOF = new(u, v, w, p, q, r);

            Assert.AreEqual(u, vector6DOF.linear.x);
            Assert.AreEqual(v, vector6DOF.linear.y);
            Assert.AreEqual(w, vector6DOF.linear.z);
            Assert.AreEqual(p, vector6DOF.angular.x);
            Assert.AreEqual(q, vector6DOF.angular.y);
            Assert.AreEqual(r, vector6DOF.angular.z);
        }

        [Test]
        public void Vector6DOF_Constructor_WithFloatArray_Success()
        {
            // Arrange
            float[] values = { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f };

            // Act
            var point = new Vector6DOF(values);

            // Assert
            Assert.AreEqual(values[0], point.u);
            Assert.AreEqual(values[1], point.v);
            Assert.AreEqual(values[2], point.w);
            Assert.AreEqual(values[3], point.p);
            Assert.AreEqual(values[4], point.q);
            Assert.AreEqual(values[5], point.r);

            // Arrange
            float[] values2 = { 1.0f, 2.0f };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Vector6DOF(values2));
        }

        [Test]
        public void Vector6DOF_Constructor_WithList_Success()
        {
            // Arrange
            var values = new List<float> { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f };

            // Act
            var point = new Vector6DOF(values);

            // Assert
            Assert.AreEqual(1.0f, point.u);
            Assert.AreEqual(2.0f, point.v);
            Assert.AreEqual(3.0f, point.w);
            Assert.AreEqual(4.0f, point.p);
            Assert.AreEqual(5.0f, point.q);
            Assert.AreEqual(6.0f, point.r);
        }

        [Test]
        public void Vector6DOF_Constructor_WithInvalidList_ThrowsException()
        {
            // Arrange
            var invalidValues = new List<float> { 1.0f, 2.0f, 3.0f }; // Not enough values

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Vector6DOF(invalidValues));
            Assert.AreEqual("The enumerable must contain exactly 6 elements.", ex.Message);
        }

        [Test]
        public void TestVector6DOFConstructorWithMatrix_1x6()
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
            Vector6DOF result = new(matrix);

            // Assert
            Assert.AreEqual(new Vector3(1, 2, 3), result.linear);
            Assert.AreEqual(new Vector3(4, 5, 6), result.angular);
        }

        [Test]
        public void TestVector6DOFConstructorWithMatrix_6x1()
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
            Vector6DOF result = new(matrix);

            // Assert
            Assert.AreEqual(new Vector3(1, 2, 3), result.linear);
            Assert.AreEqual(new Vector3(4, 5, 6), result.angular);
        }
    }
}