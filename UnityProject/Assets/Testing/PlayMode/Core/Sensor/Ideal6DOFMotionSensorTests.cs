using NUnit.Framework;
using SimuNEX;
using SimuNEX.Mechanical;
using SimuNEX.Sensors;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace SensorTests
{
    public class Ideal6DOFMotionSensorTests
    {
        private const float tolerance = 0.2f;

        private GameObject gameObject;
        private RigidBody rigidBody;
        private DynamicSystem dynamicSystem;
        private SensorSystem sensorSystem;
        private Ideal6DOFMotionSensor motionSensor;
        private Vector6DOF initialVelocity = new();

        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject("Test");
            sensorSystem = gameObject.AddComponent<SensorSystem>();

            dynamicSystem = gameObject.AddComponent<DynamicSystem>();
            rigidBody = gameObject.AddComponent<RigidBody>();

            initialVelocity = new(1, 2, 3, 2, 3, 1);
            rigidBody.enabled = false;

            rigidBody.initialVelocity = initialVelocity;
            rigidBody.enabled = true;

            GameObject sensorObject = new("Sensor");
            sensorObject.transform.SetParent(gameObject.transform);

            motionSensor = sensorObject.AddComponent<Ideal6DOFMotionSensor>();
            motionSensor.enabled = false;
            motionSensor.rigidBody = rigidBody;
            motionSensor.enabled = true;
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(gameObject);
        }

        [UnityTest]
        public IEnumerator VerifyOutputs()
        {
            dynamicSystem.Setup();

            yield return null;

            // Sensor has 6 velocities + 7 positions (quaternion + vector3) outputs
            Assert.AreEqual(13, sensorSystem.outputs.Length);

            // Sensors are updated before dynamics so we need to obtain the values after stepping
            sensorSystem.GetSensorOutputs();

            Vector6DOF currentVelocity = new(rigidBody.body.velocity, rigidBody.body.angularVelocity);

            // Sensor outputs are private so variables must be accessed through sensorSystem
            // y and z components are swapped in the sensor readings due Unity's Y-up convention
            Assert.AreEqual(currentVelocity.u, sensorSystem.outputs[0], tolerance);
            Assert.AreEqual(currentVelocity.w, sensorSystem.outputs[1], tolerance);
            Assert.AreEqual(currentVelocity.v, sensorSystem.outputs[2], tolerance);
            Assert.AreEqual(currentVelocity.p, sensorSystem.outputs[3], tolerance);
            Assert.AreEqual(currentVelocity.r, sensorSystem.outputs[4], tolerance);
            Assert.AreEqual(currentVelocity.q, sensorSystem.outputs[5], tolerance);
        }
    }
}
