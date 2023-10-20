using System;
using System.Collections.Generic;
using UnityEngine;

public class ActuatorSystem : MonoBehaviour
{
    public List<Actuator> actuators;
    private RigidBody rb;

    private void OnValidate()
    {
        rb = GetComponent<RigidBody>();
        actuators = new List<Actuator>(GetComponentsInChildren<Actuator>());

        foreach (Actuator actuator in actuators)
        {
            actuator.rb = rb;
        }
    }
}

public class Actuator : MonoBehaviour
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
}
