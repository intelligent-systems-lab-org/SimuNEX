using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Groups all attached <see cref="Actuator"/> objects.
/// </summary>
public class ActuatorSystem : MonoBehaviour
{
    public List<Actuator> actuators;
    public float[] inputs;
    private RigidBody rb;

    private void OnValidate()
    {
        rb = GetComponent<RigidBody>();
        actuators = new List<Actuator>(GetComponentsInChildren<Actuator>());
        int inputSize = 0;

        foreach (Actuator actuator in actuators)
        {
            actuator.rb = rb;
            inputSize += actuator.inputSize;
        }
        inputs = new float[inputSize];
    }
}

/// <summary>
/// Interface for implementing actuator-based systems.
/// </summary>
public abstract class Actuator : MonoBehaviour
{
    /// <summary>
    /// <see cref="RigidBody"/> object motor is attached to.
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

    public int inputSize
    {
        get => (inputs == null)? 0 : inputs.Length;
    }
}