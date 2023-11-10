using FactoryTests;
using NUnit.Framework;
using SimuNEX;
using SimuNEX.Mechanical;
using UnityEngine;

namespace LoadTests.Fins
{
    public class FinsTestsSetup : TypeRegisterSetup<Fin>
    {
    }

    public class FinForceTestsSetup : TypeRegisterSetup<Fin.FinForce>
    {
        public override bool includeNested => true;
    }

    [SetUpFixture]
    public abstract class FinTests<TFin, TForce>
        where TFin : Fin, new()
        where TForce : Fin.FinForce
    {
        protected static TFin testFin { get; private set; }

        protected GameObject spinnerObject;

        protected static RigidBody sharedRigidBody { get; private set; }

        [OneTimeSetUp]
        public void Setup()
        {
            sharedRigidBody = new GameObject("Test").AddComponent<RigidBody>();

            spinnerObject = new("Fin");
            spinnerObject.transform.SetParent(sharedRigidBody.gameObject.transform);

            testFin = sharedRigidBody.gameObject.AddComponent<TFin>();
            InitializeFin();

            // Register the fin type as being tested
            FinsTestsSetup.RegisterTest<TFin>();
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            Object.DestroyImmediate(sharedRigidBody.gameObject);
        }

        /// <summary>
        /// Sets up relevant fin parameters for testing.
        /// </summary>
        public abstract void InitializeFin();

        public abstract class FinForceTest : ForceTests.ForceTests<TForce, RigidBody>
        {
            public override void Setup()
            {
                testFin.enabled = false;
                testFin.rigidBody = sharedRigidBody;
                testFin.enabled = true;

                rigidBody = sharedRigidBody;
                forceInstance = rigidBody.gameObject.GetComponent<TForce>();
                SetProperties();
                FinForceTestsSetup.RegisterTest<TForce>();
            }
        }
    }
}
