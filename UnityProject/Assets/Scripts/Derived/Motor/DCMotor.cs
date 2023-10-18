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

    /// <summary>
    /// The time constant of the DC motor. It represents the speed 
    /// at which the motor responds to changes in voltage.
    /// </summary>
    public float timeConstant = 0.2f;

    /// <summary>
    /// The DC gain of the motor. It represents the steady-state 
    /// change in output for a given change in input voltage.
    /// </summary>
    public float DCGain = 1f;

    /// <summary>
    /// <see cref="StateSpace"/> which defines the transfer function.
    /// </summary>
    private StateSpace stateSpace = new();

    protected override void Initialize()
    {
        parameters = new Func<float>[]
        {
            () => timeConstant,
            () => DCGain
        };

        inputs = new Func<float>[] { () => voltage };

        stateSpace.Initialize
        (
            1,
            inputs.Length,
            new Matrix(inputs.Length, 1, new float[1] { 0f }),
            (states, inputs) => (1 / parameters[0]()) * (parameters[1]() * inputs - states),
            new Integrators.RK4()
        );

        MF = (inputs, parameters) =>
        {
            stateSpace.inputs[0, 0] = inputs[0]();
            stateSpace.Compute();
            return stateSpace.states[0, 0];
        };
    }
}