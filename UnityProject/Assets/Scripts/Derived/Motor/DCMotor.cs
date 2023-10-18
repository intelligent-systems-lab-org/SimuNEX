using System;
using static StateSpaceTypes;

/// <summary>
/// Implements a DC motor modeled by a 1st-order transfer function.
/// </summary>
public class DCMotor : Motor
{
    /// <summary>
    /// The input voltage.
    /// </summary>
    public float voltage = 0;

    // Motor parameters
    public float armatureResistance = 20f;
    public float backEMFConstant = 1f;
    public float torqueConstant = 10f;
    public float momentOfInertia = 1f;
    public float viscousDamping = 0;

    /// <summary>
    /// <see cref="FirstOrderTF"/> which defines the transfer function.
    /// </summary>
    private FirstOrderTF stateSpace;

    protected override void Initialize()
    {
        parameters = new Func<float>[]
        {
            () => armatureResistance, 
            () => backEMFConstant, 
            () => torqueConstant, 
            () => momentOfInertia, 
            () => viscousDamping
        };

        // Convert physical parameters to 1st order TF parameters
        float timeConstant = parameters[3]() * 1 / (parameters[4]() + (parameters[1]() * parameters[2]() / parameters[0]()));
        float DCGain = timeConstant * parameters[2]() / (parameters[0]() * parameters[3]());

        inputs = new Func<float>[] { () => voltage };

        stateSpace = new FirstOrderTF(() => timeConstant, () => DCGain);

        MF = (inputs, parameters) =>
        {
            stateSpace.Input = inputs[0]();
            stateSpace.Compute();
            return stateSpace.Output;
        };
    }
}