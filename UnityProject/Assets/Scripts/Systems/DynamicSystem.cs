using UnityEngine;

/// <summary>
/// Principal component for simulating SimuNEX models.
/// </summary>
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
        if (sensorSystem != null )
        {
            sensorSystem.GetSensorOutputs();

            if (comSystem != null )
            {
                comSystem.Send(sensorSystem.outputs);
            }
        }

        if (comSystem != null)
        { 
            comSystem.Receive(receivedData);
        }

        if (actuatorSystem != null)
        {
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

        if (dynamics != null)
        {
            dynamics.Step();
        }
    }
}
