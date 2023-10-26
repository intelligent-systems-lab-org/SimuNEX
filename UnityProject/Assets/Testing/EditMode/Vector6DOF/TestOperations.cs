using NUnit.Framework;
using UnityEngine;

namespace Vector6DOFTests
{
    public class TestOperators
    {
        [Test]
        public void OperatorPlus_AddsVectors()
        {
            // Arrange
            Vector6DOF v1 = new()
            {
                linear = new Vector3(1f, 2f, 3f),
                angular = new Vector3(4f, 5f, 6f)
            };

            Vector6DOF v2 = new()
            {
                linear = new Vector3(2f, 3f, 4f),
                angular = new Vector3(5f, 6f, 7f)
            };

            // Act
            Vector6DOF result = v1 + v2;

            // Assert
            Assert.AreEqual(3f, result.linear.x);
            Assert.AreEqual(5f, result.linear.y);
            Assert.AreEqual(7f, result.linear.z);
            Assert.AreEqual(9f, result.angular.x);
            Assert.AreEqual(11f, result.angular.y);
            Assert.AreEqual(13f, result.angular.z);
        }

        [Test]
        public void OperatorMultiply_MatrixAndVector6DOF_ReturnsVector6DOF()
        {
            // Arrange
            Matrix matrix = new(new float[,]
            {
            { 2f, 0f, 0f, 0f, 0f, 0f },
            { 0f, 2f, 0f, 0f, 0f, 0f },
            { 0f, 0f, 2f, 0f, 0f, 0f },
            { 0f, 0f, 0f, 2f, 0f, 0f },
            { 0f, 0f, 0f, 0f, 2f, 0f },
            { 0f, 0f, 0f, 0f, 0f, 2f },
            });

            Vector6DOF v = new(new Vector3(1f, 2f, 3f), new Vector3(4f, 5f, 6f));
            Vector6DOF expected = new(new Vector3(2f, 4f, 6f), new Vector3(8f, 10f, 12f));

            // Act
            Vector6DOF result = matrix * v;

            // Assert
            Assert.AreEqual(expected.linear, result.linear);
            Assert.AreEqual(expected.angular, result.angular);
        }

        [Test]
        public void ToBodyFrame_Transform_ReturnsVector6DOFInBodyFrame()
        {
            // Arrange
            Transform transform = new GameObject().transform;
            transform.position = new Vector3(1f, 2f, 3f);
            transform.rotation = Quaternion.Euler(45f, 90f, 180f);

            Vector6DOF v = new(new Vector3(1f, 2f, 3f), new Vector3(4f, 5f, 6f));
            Vector6DOF expected = new(transform.InverseTransformDirection(v.linear), transform.InverseTransformDirection(v.angular));

            // Act
            Vector6DOF result = v.ToBodyFrame(transform);

            // Assert
            Assert.AreEqual(expected.linear, result.linear);
            Assert.AreEqual(expected.angular, result.angular);
        }

        [Test]
        public void TestVector6DOFSubtraction()
        {
            Vector6DOF v1 = new(Vector3.one, Vector3.one);
            Vector6DOF v2 = new(Vector3.one, Vector3.one);
            Vector6DOF result = v1 - v2;

            Assert.AreEqual(Vector6DOF.zero, result);
        }

        [Test]
        public void TestVector6DOFMultiplication()
        {
            Vector6DOF v = new(Vector3.one, Vector3.one);
            float scalar = 2f;
            Vector6DOF result1 = v * scalar;
            Vector6DOF result2 = scalar * v;

            Assert.AreEqual(new Vector6DOF(Vector3.one * 2f, Vector3.one * 2f), result1);
            Assert.AreEqual(new Vector6DOF(Vector3.one * 2f, Vector3.one * 2f), result2);
        }

        [Test]
        public void TestVector6DOFDivision()
        {
            Vector6DOF v = new(Vector3.one * 4f, Vector3.one * 4f);
            float scalar = 2f;

            // Test v2 / scalar
            Vector6DOF result1 = v / scalar;
            Vector6DOF expected1 = new(Vector3.one * 2f, Vector3.one * 2f);
            Assert.AreEqual(expected1, result1);

            // Test scalar / v2
            Vector6DOF result2 = scalar / v;
            Vector6DOF expected2 = new(Vector3.one / 2f, Vector3.one / 2f);
            Assert.AreEqual(expected2, result2);
        }

        [Test]
        public void TestVector6DOFEquality()
        {
            Vector6DOF v1 = new(Vector3.one, Vector3.one);
            Vector6DOF v2 = new(Vector3.one, Vector3.one);
            Vector6DOF v3 = new(Vector3.zero, Vector3.zero);

            // Test equality
            Assert.IsTrue(v1.Equals(v2));
            Assert.IsTrue(v2.Equals(v1));
            Assert.IsFalse(v1.Equals(v3));
            Assert.IsFalse(v3.Equals(v1));

            // Test inequality
            Assert.IsFalse(v1.Equals(null));
            Assert.IsFalse(v1.Equals("SomeString"));

            // Test GetHashCode
            int hashCode1 = v1.GetHashCode();
            int hashCode2 = v2.GetHashCode();
            int hashCode3 = v3.GetHashCode();

            Assert.AreEqual(hashCode1, hashCode2);
            Assert.AreNotEqual(hashCode1, hashCode3);
        }

        [Test]
        public void TestVector6DOFInequality()
        {
            Vector6DOF v1 = new(Vector3.one, Vector3.one);
            Vector6DOF v2 = new(Vector3.zero, Vector3.zero);

            Assert.AreNotEqual(v1, v2);
            Assert.IsTrue(v1 != v2);
            Assert.IsFalse(v1 == v2);
        }

        [Test]
        public void TestVector6DOFElementWiseOperations()
        {
            Vector6DOF v1 = new(new Vector3(2, 4, 6), new Vector3(8, 10, 12));
            Vector6DOF v2 = new(new Vector3(3, 2, 5), new Vector3(2, 2, 2));

            Vector6DOF resultMul = v1 * v2;
            Vector6DOF resultDiv = v1 / v2;

            Assert.AreEqual(new Vector6DOF(new Vector3(6, 8, 30), new Vector3(16, 20, 24)), resultMul);
            Assert.AreEqual(new Vector6DOF(new Vector3(2.0f / 3.0f, 2, 6.0f / 5.0f), new Vector3(4, 5, 6)), resultDiv);
        }

        [Test]
        public void TestVector6DOFApply()
        {
            Vector6DOF v = new(new Vector3(1f, 2f, 3f), new Vector3(0.5f, 1f, 1.5f));

            // Applying different functions to linear and angular components
            Vector6DOF result1 = v.Apply(x => x * 2f, y => Mathf.Sin(y));
            Assert.AreEqual(new Vector6DOF(new Vector3(2f, 4f, 6f), new Vector3(0.4794255f, 0.8414709f, 0.9974949f)), result1);

            // Applying the same function to linear and angular components
            Vector6DOF result2 = v.Apply(x => Mathf.Sqrt(x));
            Assert.AreEqual(new Vector6DOF(new Vector3(1f, 1.4142135f, 1.732051f), new Vector3(0.7071068f, 1f, 1.224745f)), result2);
        }
    }
}