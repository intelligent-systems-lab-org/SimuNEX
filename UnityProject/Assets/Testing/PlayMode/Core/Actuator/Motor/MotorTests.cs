using FactoryTests;
using NUnit.Framework;
using System;
using SimuNEX;
using SimuNEX.Actuators;
using SimuNEX.Loads;
using SimuNEX.Mechanical;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace ActuatorTests.Motors
{
    public class MotorTestsSetup : TypeRegisterSetup<Motor>
    {
    }

    public class TestMotorLoad : Propeller
    {
        protected override void Initialize()
        {
            _force = rigidBody.gameObject.AddComponent<TestForce>();
            (_force as TestForce).Initialize(this);
        }

        public class TestForce : PropellerForce
        {
            public override float[] PropellerFunction(Func<float> speed, Func<float[]> parameters)
            {
                return new float[2] { speed(), speed() * 0.01f };
            }
        }
    }

    public abstract class MotorTests<TMotor> where TMotor : Motor
    {
        protected TMotor testMotor;
        protected GameObject spinnerObject;
        protected DynamicSystem dynamicSystem;
        protected ActuatorSystem actuatorSystem;
        protected RigidBody rigidBody;
        protected TestMotorLoad testLoad;

        [SetUp]
        public void Setup()
        {
            rigidBody = new GameObject("Test").AddComponent<RigidBody>();
            actuatorSystem = rigidBody.gameObject.AddComponent<ActuatorSystem>();

            dynamicSystem = rigidBody.gameObject.GetComponent<DynamicSystem>();

            spinnerObject = new("Spinner");
            spinnerObject.transform.SetParent(rigidBody.gameObject.transform);

            testLoad = rigidBody.gameObject.AddComponent<TestMotorLoad>();
            testMotor = rigidBody.gameObject.AddComponent<TMotor>();

            MotorTestsSetup.RegisterTest<TMotor>();
        }

        [TearDown]
        public void GlobalTeardown()
        {
            UnityEngine.Object.DestroyImmediate(rigidBody.gameObject);
        }

        public abstract void InitializeMotor();

        [UnityTest]
        public IEnumerator BaseTests()
        {
            dynamicSystem.Setup();
            testLoad.enabled = false;

            InitializeMotor();
            testLoad.enabled = true;
            actuatorSystem.SetActuatorInputs();

            yield return new WaitForFixedUpdate();

            Assert.IsTrue(testMotor.rigidBody != null);
            Assert.IsTrue(testLoad.rigidBody != null);
            Assert.IsTrue(testMotor.motorLoad != null);

            Assert.IsTrue(testMotor.inputSize == actuatorSystem.inputs.Length);

            for (int i = 0; i < actuatorSystem.inputs.Length; i++)
            {
                Assert.AreEqual(actuatorSystem.inputs[i], testMotor.inputs()[i]);
            }
        }
    }
}
