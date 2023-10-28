using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Groups all attached <see cref="Sensor"/> objects.
/// </summary>
public class SensorSystem : MonoBehaviour
{
    /// <summary>
    /// Connected <see cref="Sensor"/> objects.
    /// </summary>
    public List<Sensor> sensors;

    /// <summary>
    /// All <see cref="Sensor"/> readings concatenated in a array.
    /// </summary>
    public float[] outputs;

    /// <summary>
    /// Attached <see cref="RigidBody"/>.
    /// </summary>
    private RigidBody rigidBody;

    /// <summary>
    /// Total number of outputs.
    /// </summary>
    private int NumOutputs;

    private void OnValidate()
    {
        UpdateSensorList();
    }

    private void Awake()
    {
        UpdateSensorList();
    }

    /// <summary>
    /// Obtains the current list of attached <see cref="Sensor"/> objects.
    /// </summary>
    public void UpdateSensorList()
    {
        rigidBody = GetComponent<RigidBody>();
        sensors = new List<Sensor>(GetComponentsInChildren<Sensor>());

        NumOutputs = 0;
        foreach (Sensor sensor in sensors)
        {
            sensor.rigidBody = rigidBody;
            NumOutputs += sensor.outputSize;
        }
        outputs = new float[NumOutputs];
    }

    /// <summary>
    /// Gets the current <see cref="Sensor"/> values.
    /// </summary>
    public void GetSensorOutputs()
    {
        int idx = 0;
        foreach (Sensor sensor in sensors)
        {
            float[] currentActuatorInputs = sensor.GetOutput();
            Array.Copy(currentActuatorInputs, 0, outputs, idx, currentActuatorInputs.Length);
            idx += currentActuatorInputs.Length;
        }
    }
}

/// <summary>
/// Interface for implementing sensor-based systems.
/// </summary>
public abstract class Sensor : MonoBehaviour
{
    /// <summary>
    /// <see cref="RigidBody"/> object sensor is attached to.
    /// </summary>
    public RigidBody rigidBody;

    /// <summary>
    /// Inputs to the sensor.
    /// </summary>
    protected Func<float[]> outputs;

    /// <summary>
    /// Parameters specific to the sensor.
    /// </summary>
    public Func<float[]> parameters;

    /// <summary>
    /// Number of outputs specific to the <see cref="Sensor"/>.
    /// </summary>
    public int outputSize
    {
        get => (outputs == null) ? 0 : outputs().Length;
    }

    /// <summary>
    /// Sets up properties and defines the sensor's function for simulation.
    /// </summary>
    protected abstract void Initialize();

    /// <summary>
    /// Gets all outputs specific to the <see cref="Sensor"/>.
    /// </summary>
    /// <returns>The current sensor readings.</returns>
    public float[] GetOutput() => outputs();
}