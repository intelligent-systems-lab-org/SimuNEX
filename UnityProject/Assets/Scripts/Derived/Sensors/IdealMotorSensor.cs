using System;
using UnityEngine;

public class IdealMotorSensor : Sensor
{
    /// <summary>
    /// Enable position as a sensor reading.
    /// </summary>
    public bool readPosition = false;

    /// <summary>
    /// Enables torque as a sensor reading.
    /// </summary>
    public bool readTorque = false;

    /// <summary>
    /// <see cref="Motor"/> object to read speed values.
    /// </summary>
    public Motor motor;

    /// <summary>
    /// The state space for obtaining additional data such as position.
    /// </summary>
    private StateSpace stateSpace;

    /// <summary>
    /// The integration method.
    /// </summary>
    public IntegrationMethod integrator;

    protected override void Initialize()
    {
        if (readPosition || readTorque)
        {
            stateSpace = new();
            stateSpace.Initialize(2, 1, new Matrix(2, 1), integrator: StateSpace.CreateIntegrator(integrator));
            stateSpace.DerivativeFcn = (states, inputs) =>
            {
                // states[0] is position, states[1] is speed
                // Derivative of position is speed, input is acceleration
                return new Matrix(2, 1, new float[]{ states[1, 0], inputs[0, 0] });
            };

            outputs = () =>
            {
                if (motor.inputs != null)
                {
                    float speed = motor.MotorFunction(motor.inputs, motor.parameters);
                    float acceleration = (speed - stateSpace.states[1, 0]) / Time.deltaTime; 

                    stateSpace.inputs[0, 0] = acceleration;
                    stateSpace.Compute();

                    float integratedPosition = stateSpace.states[0, 0] % 2 * MathF.PI;

                    // Compute torque using provided relationship
                    float torque = motor.totalInertia * acceleration + motor.totalDamping * speed;

                    if (readPosition && readTorque)
                    {
                        return new float[] { speed, integratedPosition, torque };
                    }
                    else if (readTorque && !readPosition)
                    {
                        return new float[] { speed, torque };
                    }
                    else if (readPosition && !readTorque) {
                        return new float[] { speed, integratedPosition };
                    }
                }
                    return (readPosition && readTorque)? new float[3] : new float[2];
            };
        }
        else
        {
            outputs = () =>
            {
                if (motor.inputs != null)
                {
                    return new float[] { motor.MotorFunction(motor.inputs, motor.parameters) };
                }
                return new float[1];
            };
        }
        
    }

    private void OnValidate()
    {
        Initialize();
    }

    private void Awake()
    {
        Initialize();
    }
}
