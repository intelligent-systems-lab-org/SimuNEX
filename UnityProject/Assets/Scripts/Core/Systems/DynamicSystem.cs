using System.Text;
using SimuNEX.Mechanical;
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
        /// System that represents the model's dynamic behavior.
        /// </summary>
        public RigidBody rigidBody;

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

        protected void Start() => Setup();
        protected void OnValidate() => Setup();

        /// <summary>
        /// Automatically attaches all found components to the system.
        /// </summary>
        public void Setup()
        {
            if (TryGetComponent(out RigidBody rigidBody))
            {
                this.rigidBody = rigidBody;
            }

            if (TryGetComponent(out ActuatorSystem actuatorSystem))
            {
                this.actuatorSystem = actuatorSystem;
                this.actuatorSystem.UpdateActuatorList();
                receivedData = new float[actuatorSystem.inputs.Length];
            }

            if (TryGetComponent(out SensorSystem sensorSystem))
            {
                this.sensorSystem = sensorSystem;
                this.sensorSystem.UpdateSensorList();
            }

            if (TryGetComponent(out COMSystem comSystem))
            {
                this.comSystem = comSystem;
            }
        }

        protected void FixedUpdate() => Step();

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
            if (rigidBody != null)
            {
                rigidBody.Step();
            }
        }

        /// <summary>
        /// Resets all actuators, system states, and message data to their defaults.
        /// </summary>
        protected void Reset()
        {
            if (actuatorSystem != null)
            {
                actuatorSystem.ResetAll();
                for (int i = 0; i < receivedData.Length; i++)
                {
                    receivedData[i] = 0;
                }
            }
            if (rigidBody != null)
            {
                rigidBody.ResetAll();
            }
        }

        /// <summary>
        /// Outputs a detailed description of the <see cref="DynamicSystem"/>.
        /// </summary>
        /// <returns>The outputted info which contains details about the <see cref="DynamicSystem"/>.</returns>
        public override string ToString()
        {
            StringBuilder builder = new();

            builder.AppendLine("- DynamicSystem");

            if (actuatorSystem != null)
            {
                // Using ActuatorSystem's ToString method
                // adding indentation for hierarchical display 
                // and adjusting indentation for nested structure
                builder.Append("   ");
                builder.AppendLine(actuatorSystem.ToString().Replace("\n", "\n   ")); 
            }

            if (sensorSystem != null)
            {
                builder.Append("   ");
                builder.AppendLine(sensorSystem.ToString().Replace("\n", "\n   "));
            }

            if (rigidBody != null)
            {
                builder.AppendLine($"   - Dynamics - {rigidBody.GetType().Name}");
            }

            if (comSystem != null)
            {
                builder.AppendLine("   - COMSystem");
                string protocolType = comSystem.protocol != null ? comSystem.protocol.GetType().Name : "None";
                builder.AppendLine($"      - COMProtocol - {protocolType}");
            }

            return builder.ToString();
        }
    }
}
