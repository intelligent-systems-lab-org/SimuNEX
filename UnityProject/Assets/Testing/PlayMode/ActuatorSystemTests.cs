using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class ActuatorSystemTests
{
    private GameObject testObject;
    private ActuatorSystem actuatorSystem;

    [SetUp]
    public void Setup()
    {
        testObject = new GameObject();

        GameObject motorObject1 = new("Motor1");
        IdentityMotor motor1 = motorObject1.AddComponent<IdentityMotor>();
        motor1.transform.SetParent(testObject.transform);

        GameObject motorObject2 = new("Motor2");
        IdentityMotor motor2 = motorObject2.AddComponent<IdentityMotor>();
        motor2.transform.SetParent(testObject.transform);

        GameObject motorObject3 = new("Motor3");
        IdentityMotor motor3 = motorObject3.AddComponent<IdentityMotor>();
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

        actuatorSystem.SetInputs();

        // Update the scene for 1 frame
        yield return null;

        actuatorSystem.GetInputs();
        float[] retrievedInputs = actuatorSystem.inputs;

        Assert.AreEqual(initialInputs.Length, retrievedInputs.Length);
        for (int i = 0; i < initialInputs.Length; i++)
        {
            Assert.AreEqual(initialInputs[i], retrievedInputs[i], 0.0001f);
        }
    }
}
