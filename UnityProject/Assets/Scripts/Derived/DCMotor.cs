using System;

public class DCMotor : Motor
{
    public float voltage = 0;
    public float timeConstant = 0.2f;
    public float DCGain = 1f;

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
            new Matrix(1, 0, new float[1] { 0f }),
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