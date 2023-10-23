using UnityEngine;

[RequireComponent(typeof(ActuatorSystem))]
[RequireComponent(typeof(SensorSystem))]
[RequireComponent(typeof(COMSystem))]
public class DynamicSystem : MonoBehaviour
{
    /// <summary>
    /// <see cref="Dynamics"/> object attached for simulation.
    /// </summary>
    Dynamics dynamics;

    /// <summary>
    /// Connected supervisory actuator system.
    /// </summary>
    ActuatorSystem actuatorSystem;

    /// <summary>
    /// Connected supervisory sensor system.
    /// </summary>
    SensorSystem sensorSystem;

    /// <summary>
    /// Enables communication for passing data in and out of SimuNEX.
    /// </summary>
    COMSystem comSystem;

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
        sensorSystem.GetSensorOutputs();

        comSystem.Send(sensorSystem.outputs);
        comSystem.Receive(receivedData);

        actuatorSystem.inputs = receivedData;
        actuatorSystem.SetActuatorInputs();

        if (dynamics!= null)
        {
            dynamics.Step();
        }
    }
}
