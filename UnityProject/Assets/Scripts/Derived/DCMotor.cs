using System;

/// <summary>
/// Implements a DC motor modeled by a 1st-order transfer function.
/// </summary>
public class DCMotor : Motor
{
    /// <summary>
    /// The input voltage.
    /// </summary>
    public float voltage = 0;

    public float timeConstant = 0.2f;
    public float DCGain = 1f;

    /// <summary>
    /// <see cref="StateSpace"/> which defines the transfer function.
    /// </summary>
    private StateSpace stateSpace = new();

    protected override void Initialize()
    {
        parameters = new Func<float>[2] 
        { 
            () => timeConstant,
            () => DCGain 
        };

        inputs = new Func<float>[1] { () => voltage };

        stateSpace.Initialize
        (
            1,
            1,
            new Matrix(1, 1, new float[1] { 0f }),
            (states, inputs) => (1 / parameters[0]()) * (parameters[1]() * inputs - states),
            new Integrators.RK4()
        );

        // Offending function
        MF = (inputs, parameters) =>
        {
            stateSpace.inputs[0, 0] = inputs[0]();
            stateSpace.Compute();
            return stateSpace.states[0, 0];
        };
    }
}