using NUnit.Framework;
using SimuNEX.Mechanical;
using UnityEngine;

namespace MechanicalTests.ForceTests
{
    public class BaseTests
    {
        private GameObject gameObject;
        private RigidBody rigidBody;
        private ContinuousForce force;

        [SetUp]
        public void SetUp()
        {
            gameObject = new("Test");
            rigidBody = gameObject.AddComponent<RigidBody>();

            force = rigidBody.gameObject.AddComponent<ContinuousForce>();
        }

        [Test]
        public void OnEnable_AddsForceToList()
        {
            // Act
            force.enabled = true;

            // Assert
            Assert.IsTrue(rigidBody.forces.Contains(force));
        }

        [Test]
        public void OnDisable_RemovesForceFromList()
        {
            // Act
            force.enabled = false;

            // Assert
            Assert.IsFalse(rigidBody.forces.Contains(force));
        }

        [Test]
        public void OnEnable_GivesWarningWithoutRigidBody()
        {
            // Arrange
            GameObject testObject = new("Test");

            // Act & Assert
            ContinuousForce testForce = testObject.AddComponent<ContinuousForce>();

            // Assert
            Assert.IsFalse(testForce.rigidBody != null);
        }
    }
}
