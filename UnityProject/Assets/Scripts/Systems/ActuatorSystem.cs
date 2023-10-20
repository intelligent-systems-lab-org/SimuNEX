using System;
using System.Collections.Generic;
using UnityEngine;

public class ActuatorSystem : MonoBehaviour
{
    public List<Actuator> actuators;
    private Dynamics dynamics;

    private void OnValidate()
    {
        dynamics = GetComponent<Dynamics>();
        actuators = new List<Actuator>(GetComponentsInChildren<Actuator>());
    }
}

public class Actuator : MonoBehaviour
{
    /// <summary>
    /// Inputs to the actuator.
    /// </summary>
    protected Func<float>[] inputs;

    /// <summary>
    /// Parameters specific to the actuator.
    /// </summary>
    protected Func<float>[] parameters;
}
