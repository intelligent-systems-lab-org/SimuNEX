using NUnit.Framework;
using SimuNEX;
using System;
using UnityEngine;

namespace Vector6DOFTests
{
    public class TestProperties
    {
        [Test]
        public void Zero_Property_Returns_Vector6DOF_With_Zero_Components()
        {
            Vector6DOF zero = Vector6DOF.zero;

            Assert.AreEqual(Vector3.zero, zero.linear);
            Assert.AreEqual(Vector3.zero, zero.angular);
        }

        [Test]
        public void TestVector6DOFIndexer()
        {
            Vector6DOF v = new(Vector3.one, Vector3.one);

            Assert.AreEqual(1f, v[0]);
            Assert.AreEqual(1f, v[1]);
            Assert.AreEqual(1f, v[2]);
            Assert.AreEqual(1f, v[3]);
            Assert.AreEqual(1f, v[4]);
            Assert.AreEqual(1f, v[5]);

            v[0] = 2f;
            v[1] = 3f;
            v[2] = 4f;
            v[3] = 5f;
            v[4] = 6f;
            v[5] = 19f;

            Assert.AreEqual(2f, v[0]);
            Assert.AreEqual(3f, v[1]);
            Assert.AreEqual(4f, v[2]);
            Assert.AreEqual(5f, v[3]);
            Assert.AreEqual(6f, v[4]);
            Assert.AreEqual(19f, v[5]);

            // Test out of range exception
            Vector6DOF v2 = new(Vector3.zero, Vector3.zero);

            _ = Assert.Throws<IndexOutOfRangeException>(() => { float result = v2[-1]; });
            _ = Assert.Throws<IndexOutOfRangeException>(() => { float result = v2[6]; });

            _ = Assert.Throws<IndexOutOfRangeException>(() => v2[-1] = 1f);
            _ = Assert.Throws<IndexOutOfRangeException>(() => v2[6] = 1f);
        }

        [Test]
        public void TestVector6DOFTranspose()
        {
            Vector6DOF v = new(new Vector3(1f, 2f, 3f), new Vector3(4f, 5f, 6f));
            Matrix transpose = v.transpose;

            Matrix test = new(1, 6, new float[] { 1f, 2f, 3f, 4f, 5f, 6f });

            Assert.IsTrue(test.IsEqual(transpose));
        }

        [Test]
        public void TestVector6DOFMagnitude()
        {
            Vector6DOF v = new(Vector3.one * 2f, Vector3.one * 3f);
            (float linearMagnitude, float angularMagnitude) = v.magnitude;

            Assert.That(2f * Mathf.Sqrt(3), Is.EqualTo(linearMagnitude).Within(1e-6));
            Assert.That(3f * Mathf.Sqrt(3), Is.EqualTo(angularMagnitude).Within(1e-6));
        }

        [Test]
        public void TestVector6DOFComponentAccessors()
        {
            Vector6DOF v = new(Vector3.one, Vector3.one * 2f);

            Assert.AreEqual(1f, v.u);
            Assert.AreEqual(1f, v.v);
            Assert.AreEqual(1f, v.w);
            Assert.AreEqual(2f, v.p);
            Assert.AreEqual(2f, v.q);
            Assert.AreEqual(2f, v.r);

            v.u = 3f;
            v.v = 4f;
            v.w = 5f;
            v.p = 6f;
            v.q = 7f;
            v.r = 8f;

            Assert.AreEqual(3f, v.u);
            Assert.AreEqual(4f, v.v);
            Assert.AreEqual(5f, v.w);
            Assert.AreEqual(6f, v.p);
            Assert.AreEqual(7f, v.q);
            Assert.AreEqual(8f, v.r);
        }

        [Test]
        public void TestGetComponent()
        {
            Vector6DOF v = new(new Vector3(1, 2, 3), new Vector3(4, 5, 6));

            // Test getting individual components
            Assert.AreEqual(1, v.Select("u")[0]);
            Assert.AreEqual(2, v.Select("v")[0]);
            Assert.AreEqual(3, v.Select("w")[0]);
            Assert.AreEqual(4, v.Select("p")[0]);
            Assert.AreEqual(5, v.Select("q")[0]);
            Assert.AreEqual(6, v.Select("r")[0]);

            // Test getting multiple components
            float[] result = v.Select("u w q");
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(3, result[1]);
            Assert.AreEqual(5, result[2]);

            // Test invalid component
            _ = Assert.Throws<NotSupportedException>(() => _ = v.Select("x"));
        }

        [Test]
        public void TestSetComponent()
        {
            Vector6DOF v = new();

            // Test setting individual components
            v.Alter("u", new float[] { 1 });
            v.Alter("v", new float[] { 2 });
            v.Alter("w", new float[] { 3 });
            v.Alter("p", new float[] { 4 });
            v.Alter("q", new float[] { 5 });
            v.Alter("r", new float[] { 6 });

            Assert.AreEqual(new Vector6DOF(new Vector3(1, 2, 3), new Vector3(4, 5, 6)), v);

            // Test setting multiple components
            v.Alter("u w q", new float[] { 10, 20, 30 });
            Assert.AreEqual(new Vector6DOF(new Vector3(10, 2, 20), new Vector3(4, 30, 6)), v);

            // Test invalid component
            _ = Assert.Throws<NotSupportedException>(() => v.Alter("x", new float[] { 1 }));

            // Test size mismatch
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => v.Alter("u w", new float[] { 1, 2, 3 }));
            Assert.AreEqual("Size mismatch: Number of components in the query must match the size of the array.", exception.Message);
        }

        [Test]
        public void SetComponents_Success()
        {
            Vector6DOF v1 = new();
            v1["u v w p q r"] = "[1; 2; 3; 4; 5; 6]";

            Assert.AreEqual(1.0f, v1.linear.x);
            Assert.AreEqual(2.0f, v1.linear.y);
            Assert.AreEqual(3.0f, v1.linear.z);
            Assert.AreEqual(4.0f, v1.angular.x);
            Assert.AreEqual(5.0f, v1.angular.y);
            Assert.AreEqual(6.0f, v1.angular.z);

            Vector6DOF v2 = new();
            _ = Assert.Throws<InvalidOperationException>(() => v2["u v w p q r"] = "[1; 2; 3; 4; 5]");

            Vector6DOF v3 = new();
            _ = Assert.Throws<InvalidOperationException>(() => v3["u v w p q r"] = "[1; 2; 3; 4; 5; 6; 7]");

            Vector6DOF v4 = new();
            _ = Assert.Throws<NotSupportedException>(() => v3["u v w x q r"] = "[1; 2; 3; 4; 5; 6]");
        }

        [Test]
        public void One_Property_Returns_Vector6DOF_With_Unit_Components()
        {
            Vector6DOF one = Vector6DOF.one;

            Assert.AreEqual(Vector3.one, one.linear);
            Assert.AreEqual(Vector3.one, one.angular);
        }
    }
}
