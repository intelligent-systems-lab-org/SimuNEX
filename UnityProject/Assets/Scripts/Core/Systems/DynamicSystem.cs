using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Serves as an orchestrator for simulating SimuNEX models, managing the interactions
    /// between dynamics, actuators, sensors, and communication systems.
    /// </summary>
    [DisallowMultipleComponent]
    public class DynamicSystem : MonoBehaviour
    {
        /// <summary>
        /// Dynamics object that represents the model's dynamic behavior.
        /// </summary>
        public Dynamics dynamics;

        /// <summary>
        /// Represents the actuation system controlling the dynamics.
        /// </summary>
        public ActuatorSystem actuatorSystem;

        /// <summary>
        /// Represents the sensing system reading states or outputs from the dynamics.
        /// </summary>
        public SensorSystem sensorSystem;

        /// <summary>
        /// Represents the communication interface for the dynamic system.
        /// </summary>
        public COMSystem comSystem;

        /// <summary>
        /// Data received from the communication system to be passed to actuators.
        /// </summary>
        public float[] receivedData;

        protected void OnEnable()
        {
            Setup();
        }

        protected void OnValidate()
        {
            Setup();
        }

        /// <summary>
        /// Automatically attaches all found components to the system.
        /// </summary>
        public void Setup()
        {
            if (dynamics == null)
            {
                dynamics = GetComponent<Dynamics>();
            }

            if (actuatorSystem == null)
            {
                actuatorSystem = GetComponent<ActuatorSystem>();
            }

            if (actuatorSystem != null)
            {
                actuatorSystem.UpdateActuatorList();
                receivedData = new float[actuatorSystem.inputs.Length];
            }

            if (sensorSystem == null)
            {
                sensorSystem = GetComponent<SensorSystem>();
            }

            if (sensorSystem != null)
            {
                sensorSystem.UpdateSensorList();
            }

            if (comSystem == null)
            {
                comSystem = GetComponent<COMSystem>();
            }
        }

        protected void FixedUpdate()
        {
            Step();
        }

        /// <summary>
        /// Updates each component at the current timestep.
        /// </summary>
        /// <remarks>This function is to be called once per timestep.</remarks>
        public void Step()
        {
            HandleSensors();
            HandleCommunication();
            HandleActuators();
            HandleDynamics();
        }

        /// <summary>
        /// Handles sensor outputs and sends them if a communication system is available.
        /// </summary>
        private void HandleSensors()
        {
            if (sensorSystem == null)
            {
                return;
            }

            sensorSystem.GetSensorOutputs();

            if (comSystem != null)
            {
                comSystem.Send(sensorSystem.outputs);
            }
        }

        /// <summary>
        /// Handles data reception from the communication system.
        /// </summary>
        private void HandleCommunication()
        {
            if (comSystem == null)
            {
                return;
            }

            comSystem.Receive(receivedData);
        }

        /// <summary>
        /// Sets actuator inputs either from received data or directly from the actuator system.
        /// </summary>
        private void HandleActuators()
        {
            if (actuatorSystem == null)
            {
                return;
            }

            if (comSystem != null)
            {
                actuatorSystem.inputs = receivedData;
            }
            else
            {
                actuatorSystem.GetActuatorInputs();
            }

            actuatorSystem.SetActuatorInputs();
        }

        /// <summary>
        /// Steps through the dynamics simulation.
        /// </summary>
        private void HandleDynamics()
        {
            if (dynamics != null)
            {
                dynamics.Step();
            }
        }
    }
}
