using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Groups all attached <see cref="Actuator"/> objects.
/// </summary>
public class ActuatorSystem : MonoBehaviour
{
    /// <summary>
    /// Connected <see cref="Actuator"/> objects.
    /// </summary>
    public List<Actuator> actuators;

    /// <summary>
    /// All <see cref="Actuator"/> inputs concatenated in a array.
    /// </summary>
    public float[] inputs;

    /// <summary>
    /// Attached <see cref="RigidBody"/>.
    /// </summary>
    private RigidBody rb;

    /// <summary>
    /// Total number of inputs.
    /// </summary>
    private int NumInputs;

    private void OnValidate()
    {
        UpdateActuatorList();
    }

    private void Awake()
    {
        UpdateActuatorList();
    }

    /// <summary>
    /// Obtains the current list of attached <see cref="Actuator"/> objects.
    /// </summary>
    public void UpdateActuatorList()
    {
        rb = GetComponent<RigidBody>();
        actuators = new List<Actuator>(GetComponentsInChildren<Actuator>());

        NumInputs = 0;
        foreach (Actuator actuator in actuators)
        {
            actuator.rb = rb;
            NumInputs += actuator.inputSize;
        }
        inputs = new float[NumInputs];
    }

    /// <summary>
    /// Sets the <see cref="Actuator"/> inputs to the assigned values.
    /// </summary>
    public void SetActuatorInputs()
    {
        int idx = 0;
        foreach (Actuator actuator in actuators)
        {
            float[] slice = inputs.Skip(idx).Take(actuator.inputSize).ToArray();
            actuator.SetInput(slice);
            idx += actuator.inputSize;
        }
    }

    /// <summary>
    /// Gets the current <see cref="Actuator"/> input values.
    /// </summary>
    public void GetActuatorInputs()
    {
        int idx = 0;
        foreach (Actuator actuator in actuators)
        {
            float[] currentActuatorInputs = actuator.GetInput();
            Array.Copy(currentActuatorInputs, 0, inputs, idx, currentActuatorInputs.Length);
            idx += currentActuatorInputs.Length;
        }
    }
}

/// <summary>
/// Interface for implementing actuator-based systems.
/// </summary>
public abstract class Actuator : MonoBehaviour
{
    /// <summary>
    /// <see cref="RigidBody"/> object actuator is attached to.
    /// </summary>
    public RigidBody rb;

    /// <summary>
    /// Inputs to the actuator.
    /// </summary>
    protected Func<float>[] inputs;

    /// <summary>
    /// Parameters specific to the actuator.
    /// </summary>
    protected Func<float>[] parameters;

    /// <summary>
    /// Number of inputs specific to the <see cref="Actuator"/>.
    /// </summary>
    public int inputSize
    {
        get => (inputs == null) ? 0 : inputs.Length;
    }

    /// <summary>
    /// Sets up properties and defines the actuator's function for simulation.
    /// </summary>
    protected abstract void Initialize();

    /// <summary>
    /// Gets all inputs specific to the <see cref="Actuator"/>.
    /// </summary>
    /// <returns>The current input values.</returns>
    public abstract float[] GetInput();

    /// <summary>
    /// Sets all inputs specific to the <see cref="Actuator"/>.
    /// </summary>
    /// <param name="value">The input values to set.</param>
    public abstract void SetInput(float[] value);
}