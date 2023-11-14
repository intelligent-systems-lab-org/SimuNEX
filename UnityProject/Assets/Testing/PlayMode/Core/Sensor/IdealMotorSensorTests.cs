using NUnit.Framework;
using SimuNEX;
using SimuNEX.Actuators;
using SimuNEX.Loads;
using SimuNEX.Mechanical;
using SimuNEX.Sensors;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace SensorTests
{
    public class Ideal6DOFMotorSensorTests
    {
        public struct MotorSensorTestCase
        {
            public bool readPosition;
            public bool readTorque;

            public MotorSensorTestCase(bool readPosition, bool readTorque)
            {
                this.readPosition = readPosition;
                this.readTorque = readTorque;
            }

            public override string ToString()
            {
                return $"ReadPosition: {readPosition}, ReadTorque: {readTorque}";
            }
        }

        public static IEnumerable MotorSensorTestCases()
        {
            yield return new MotorSensorTestCase(true, false);
            yield return new MotorSensorTestCase(false, true);
            yield return new MotorSensorTestCase(true, true);
            yield return new MotorSensorTestCase(false, false);
        }

        private const float tolerance = 0.2f;

        private GameObject gameObject;
        private GameObject spinnerObject;
        private RigidBody rigidBody;
        private ActuatorSystem actuatorSystem;
        private DynamicSystem dynamicSystem;
        private SensorSystem sensorSystem;
        private DCMotor testMotor;
        private SimplePropeller testLoad;
        private IdealMotorSensor motorSensor;

        private const float inputVoltage = 100f;

        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject("Test");
            rigidBody = gameObject.AddComponent<RigidBody>();
            spinnerObject = new("Spinner");
            spinnerObject.transform.SetParent(rigidBody.gameObject.transform);
            testMotor = gameObject.AddComponent<DCMotor>();
            testLoad = gameObject.AddComponent<SimplePropeller>();
            dynamicSystem = gameObject.GetComponent<DynamicSystem>();

            GameObject sensorObject = new("Sensor");
            sensorObject.transform.SetParent(gameObject.transform);

            motorSensor = sensorObject.AddComponent<IdealMotorSensor>();
            motorSensor.motor = testMotor;
            motorSensor.rigidBody = rigidBody;
            motorSensor.enabled = false;

            sensorSystem = gameObject.AddComponent<SensorSystem>();
            actuatorSystem = gameObject.AddComponent<ActuatorSystem>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(gameObject);
        }

        [UnityTest]
        public IEnumerator VerifyOutputs([ValueSource(nameof(MotorSensorTestCases))] MotorSensorTestCase testCase)
        {
            motorSensor.readPosition = testCase.readPosition;
            motorSensor.readTorque = testCase.readTorque;

            testLoad.enabled = false;
            testLoad.enabled = true;

            testMotor.SetUp();
            testMotor.voltage = inputVoltage;

            motorSensor.enabled = true;

            dynamicSystem.Setup();

            actuatorSystem.SetActuatorInputs();

            yield return new WaitForFixedUpdate();

            Assert.IsTrue(testMotor.rigidBody != null);
            Assert.IsTrue(testLoad.rigidBody != null);
            Assert.IsTrue(testMotor.motorLoad != null);

            Assert.IsTrue(testMotor.inputSize == actuatorSystem.inputs.Length);

            // Sensors are updated before dynamics so we need to obtain the values after stepping
            sensorSystem.GetSensorOutputs();

            if (testCase.readPosition && testCase.readTorque)
            {
                // Sensor has 3 outputs, motorSpeed, motorPosition, and motorTorque
                Assert.AreEqual(3, sensorSystem.outputs.Length);
            }
            else if (testCase.readPosition || testCase.readTorque)
            {
                // Sensor has 2 outputs, motorSpeed and either motorPosition or motorTorque
                Assert.AreEqual(2, sensorSystem.outputs.Length);

                if (testCase.readPosition)
                {
                    Assert.AreEqual(testMotor.motorLoad.normalizedAngle, sensorSystem.outputs[1]);
                }
            }
            else
            {
                // Sensor has 1 output, motorSpeed
                Assert.AreEqual(1, sensorSystem.outputs.Length);
            }

            // Sensor outputs are private so variables must be accessed through sensorSystem
            Assert.AreEqual(testMotor.motorLoad._speed, sensorSystem.outputs[0], tolerance);
        }
    }
}
