using NUnit.Framework;
using SimuNEX;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace CoreTests
{
    public class ActuatorSystemTests
    {
        private GameObject testObject;
        private ActuatorSystem actuatorSystem;

        [SetUp]
        public void Setup()
        {
            testObject = new GameObject();

            GameObject motorObject1 = new("Motor1");
            IdentityActuator motor1 = motorObject1.AddComponent<IdentityActuator>();
            motor1.transform.SetParent(testObject.transform);

            GameObject motorObject2 = new("Motor2");
            IdentityActuator motor2 = motorObject2.AddComponent<IdentityActuator>();
            motor2.transform.SetParent(testObject.transform);

            GameObject motorObject3 = new("Motor3");
            IdentityActuator motor3 = motorObject3.AddComponent<IdentityActuator>();
            motor3.transform.SetParent(testObject.transform);

            actuatorSystem = testObject.AddComponent<ActuatorSystem>();
        }

        [TearDown]
        public void Teardown()
        {
            // Destroy the GameObject after the test is complete
            Object.DestroyImmediate(testObject);
        }

        [UnityTest]
        public IEnumerator TestGetAndSetInputs()
        {
            float[] initialInputs = new float[] { 1.0f, 2.0f, 3.0f };
            actuatorSystem.inputs = initialInputs;

            actuatorSystem.SetActuatorInputs();

            // Update the scene for 1 frame
            yield return null;

            actuatorSystem.GetActuatorInputs();
            float[] retrievedInputs = actuatorSystem.inputs;

            Assert.AreEqual(initialInputs.Length, retrievedInputs.Length);
            for (int i = 0; i < initialInputs.Length; i++)
            {
                Assert.AreEqual(initialInputs[i], retrievedInputs[i], 0.0001f);
            }
        }
    }
}
