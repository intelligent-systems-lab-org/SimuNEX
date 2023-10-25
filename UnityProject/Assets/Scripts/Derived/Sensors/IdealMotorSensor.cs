using System;

public class IdealMotorSensor : Sensor
{
    /// <summary>
    /// Enable position as a sensor reading.
    /// </summary>
    public bool readPosition = false;

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
        stateSpace = new();
        stateSpace.Initialize(1, 1, new Matrix(1, 1), integrator: StateSpace.CreateIntegrator(integrator));
        stateSpace.DerivativeFcn = (states, inputs) => inputs;

        if (readPosition)
        {
            outputs = () =>
            {
                if (motor.inputs != null)
                {
                    float speed = motor.MotorFunction(motor.inputs, motor.parameters);
                    stateSpace.inputs[0, 0] = speed;
                    stateSpace.Compute();
                    float integratedPosition = stateSpace.states[0, 0] % 2 * MathF.PI;
                    return new float[] { speed, integratedPosition };
                }
                return new float[2];
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
