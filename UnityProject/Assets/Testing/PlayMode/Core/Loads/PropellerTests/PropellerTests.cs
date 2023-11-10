using FactoryTests;
using NUnit.Framework;
using SimuNEX;
using SimuNEX.Mechanical;
using UnityEngine;

namespace LoadTests.Propellers
{
    public class PropellerTestsSetup : TypeRegisterSetup<Propeller>
    {
    }

    public class PropellerForceTestsSetup : TypeRegisterSetup<Propeller.PropellerForce>
    {
        public override bool includeNested => true;
    }

    [SetUpFixture]
    public abstract class PropellerTests<TPropeller, TForce>
        where TPropeller : Propeller, new()
        where TForce : Propeller.PropellerForce
    {
        protected static TPropeller testPropeller { get; private set; }

        protected GameObject spinnerObject;

        protected static RigidBody sharedRigidBody { get; private set; }

        [OneTimeSetUp]
        public void Setup()
        {
            sharedRigidBody = new GameObject("Test").AddComponent<RigidBody>();

            spinnerObject = new("Propeller");
            spinnerObject.transform.SetParent(sharedRigidBody.gameObject.transform);

            testPropeller = sharedRigidBody.gameObject.AddComponent<TPropeller>();
            InitializePropeller();

            // Register the propeller type as being tested
            PropellerTestsSetup.RegisterTest<TPropeller>();
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            Object.DestroyImmediate(sharedRigidBody.gameObject);
        }

        /// <summary>
        /// Sets up relevant propeller parameters for testing.
        /// </summary>
        public abstract void InitializePropeller();

        public abstract class PropellerForceTest : ForceTests.ForceTests<TForce, RigidBody>
        {
            public override void Setup()
            {
                testPropeller.enabled = false;
                testPropeller.rigidBody = sharedRigidBody;
                testPropeller.enabled = true;

                rigidBody = sharedRigidBody;
                forceInstance = rigidBody.gameObject.GetComponent<TForce>();
                SetProperties();
                PropellerForceTestsSetup.RegisterTest<TForce>();
            }
        }
    }
}
