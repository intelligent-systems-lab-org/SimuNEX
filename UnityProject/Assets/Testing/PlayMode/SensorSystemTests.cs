using NUnit.Framework;
using SimuNEX;
using System.Collections;
using SimuNEX.Mechanical;
using UnityEngine;
using UnityEngine.TestTools;

namespace SimuNEXTests
{
    public class SensorSystemTests
    {
        private GameObject testObject;
        private SensorSystem sensorSystem;
        private RigidBody rigidBody;

        public class TestSensor : Sensor
        {
            public float[] values;

            protected override void Initialize()
            {
                outputs = () => values;
            }

            public float[] Outputs
            {
                set => values = value;
            }

            protected void OnValidate()
            {
                Initialize();
            }

            protected void Awake()
            {
                Initialize();
            }
        }

        [SetUp]
        public void Setup()
        {
            testObject = new GameObject();
            rigidBody = testObject.AddComponent<RigidBody>();

            GameObject sensorObject1 = new("Sensor1");
            TestSensor sensor1 = sensorObject1.AddComponent<TestSensor>();
            sensor1.transform.SetParent(testObject.transform);
            sensor1.Outputs = new float[3] { 1, 2, 3 };

            GameObject sensorObject2 = new("Sensor2");
            TestSensor sensor2 = sensorObject2.AddComponent<TestSensor>();
            sensor2.transform.SetParent(testObject.transform);
            sensor2.Outputs = new float[5] { 4, 5, 6, 7, 8 };

            sensorSystem = testObject.AddComponent<SensorSystem>();
        }

        [TearDown]
        public void Teardown()
        {
            // Destroy the GameObject after the test is complete
            Object.DestroyImmediate(testObject);
        }

        [UnityTest]
        public IEnumerator TestGetOutputs()
        {
            float[] initialOutputs = new float[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            // Update the scene for 1 frame
            yield return null;

            sensorSystem.GetSensorOutputs();
            float[] retrievedOutputs = sensorSystem.outputs;

            Assert.AreEqual(initialOutputs.Length, retrievedOutputs.Length);
            for (int i = 0; i < initialOutputs.Length; i++)
            {
                Assert.AreEqual(initialOutputs[i], retrievedOutputs[i], 0.0001f);
            }
        }
    }
}
