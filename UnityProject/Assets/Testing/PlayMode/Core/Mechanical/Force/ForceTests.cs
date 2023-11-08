using FactoryTests;
using NUnit.Framework;
using SimuNEX;
using SimuNEX.Mechanical;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace ForceTests
{
    public class ForceTestsSetup : TypeRegisterSetup<Force>
    {
    }

    public abstract class ForceTests<TForce> where TForce : Force, new()
    {
        protected TForce forceInstance;

        /// <summary>
        /// Test tolerance value.
        /// </summary>
        protected virtual float tolerance => 0.01f;

        protected GameObject gameObject;

        /// <summary>
        /// Test <see cref="RigidBody"/>.
        /// </summary>
        protected RigidBody rigidBody;

        /// <summary>
        /// Expected value for the test.
        /// </summary>
        protected abstract Vector6DOF expectedValue { get; }

        /// <summary>
        /// Set <see cref="Force"/> specific properties where necessary.
        /// </summary>
        protected abstract void SetProperties();

        [SetUp]
        public void SetUp()
        {
            gameObject = new("Test");
            rigidBody = gameObject.AddComponent<RigidBody>();

            forceInstance = rigidBody.gameObject.AddComponent<TForce>();
            SetProperties();

            // Register the fault type as being tested
            ForceTestsSetup.RegisterTest<TForce>();
        }

        [UnityTest]
        public virtual IEnumerator TestForce()
        {
            rigidBody.Step();

            yield return null;

            IsForceApplied();
        }

        /// <summary>
        /// Verifies if the <see cref="Force"/> was applied to the <see cref="RigidBody"/>.
        /// </summary>
        /// <returns>True if the force applied to the <see cref="RigidBody"/> meets the required critera.
        /// False otherwise.</returns>
        public bool IsForceApplied()
        {
            Vector6DOF difference = expectedValue - rigidBody.appliedForce;

            Assert.That(Mathf.Abs(difference.u), Is.LessThan(tolerance), "u component out of tolerance");
            Assert.That(Mathf.Abs(difference.v), Is.LessThan(tolerance), "v component out of tolerance");
            Assert.That(Mathf.Abs(difference.w), Is.LessThan(tolerance), "w component out of tolerance");
            Assert.That(Mathf.Abs(difference.p), Is.LessThan(tolerance), "p component out of tolerance");
            Assert.That(Mathf.Abs(difference.q), Is.LessThan(tolerance), "q component out of tolerance");
            Assert.That(Mathf.Abs(difference.r), Is.LessThan(tolerance), "r component out of tolerance");

            return true;
        }
    }
}
