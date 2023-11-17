using NUnit.Framework;
using SimuNEX.Mechanical;
using System;
using UnityEngine;

namespace MechanicalTests
{
    public class RigidBodyTests
    {
        private GameObject testObject;
        private RigidBody rigidBody;

        private const float testMass = 10f;

        [SetUp]
        public void SetUp()
        {
            testObject = new("Test");
            rigidBody = testObject.AddComponent<RigidBody>();
        }

        [Test]
        public void MassProperty_SetsCorrectly()
        {
            // Act
            rigidBody.mass = testMass;

            // Assert
            Assert.AreEqual(testMass, rigidBody.mass);
        }

        [Test]
        public void MassProperty_NegativeValue_ThrowsArgumentException()
        {
            // Act & Assert
            _ = Assert.Throws<ArgumentException>(() => rigidBody.mass = -testMass);
        }

        [Test]
        public void AngularPosition_ReturnsTransformRotation()
        {
            Quaternion testRotation = Quaternion.identity;
            testObject.transform.rotation = testRotation;

            Assert.IsTrue(rigidBody.angularPosition == testRotation);
        }

        [Test]
        public void Position_ReturnsTransformPosition()
        {
            Vector3 testPosition = new(10, 5, 10);
            testObject.transform.position = testPosition;

            Assert.IsTrue(rigidBody.position == testPosition);
        }
    }
}
