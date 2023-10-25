using UnityEngine;

/// <summary>
/// Serves as an orchestrator for simulating SimuNEX models, managing the interactions 
/// between dynamics, actuators, sensors, and communication systems.
/// </summary>
public class DynamicSystem : MonoBehaviour
{
    /// <summary>
    /// Dynamics object that represents the model's dynamic behavior.
    /// </summary>
    Dynamics dynamics;

    /// <summary>
    /// Represents the actuation system controlling the dynamics.
    /// </summary>
    ActuatorSystem actuatorSystem;

    /// <summary>
    /// Represents the sensing system reading states or outputs from the dynamics.
    /// </summary>
    SensorSystem sensorSystem;

    /// <summary>
    /// Represents the communication interface for the dynamic system.
    /// </summary>
    COMSystem comSystem;

    /// <summary>
    /// Data received from the communication system to be passed to actuators.
    /// </summary>
    public float[] receivedData;

    void Awake()
    {
        dynamics = GetComponent<Dynamics>();
        actuatorSystem = GetComponent<ActuatorSystem>();
        sensorSystem = GetComponent<SensorSystem>();
        comSystem = GetComponent<COMSystem>();

        receivedData = new float[actuatorSystem.inputs.Length];
    }

    private void FixedUpdate()
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
        if (sensorSystem == null) return;

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
        if (comSystem == null) return;

        comSystem.Receive(receivedData);
    }

    /// <summary>
    /// Sets actuator inputs either from received data or directly from the actuator system.
    /// </summary>
    private void HandleActuators()
    {
        if (actuatorSystem == null) return;

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