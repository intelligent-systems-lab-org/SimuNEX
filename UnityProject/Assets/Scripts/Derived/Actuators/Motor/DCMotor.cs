using System;
using static StateSpaceTypes;
using UnityEngine;

/// <summary>
/// Implements a DC motor modeled by a 1st-order transfer function.
/// </summary>
public class DCMotor : Motor
{
    /// <summary>
    /// The integration method.
    /// </summary>
    public IntegrationMethod integrator;

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

    public override void SetInput(float[] value) => voltage = value[0];

    protected override void Initialize()
    {
        parameters = () => new float[]
        {
            armatureResistance, backEMFConstant, torqueConstant, momentOfInertia, viscousDamping
        };

        // Convert physical parameters to 1st order TF parameters
        float timeConstant()
        {
            float[] param = parameters();
            return param[3] * 1 / (param[4] + (param[1] * param[2] / param[0])); 
        };
        float DCGain() 
        {
            float[] param = parameters();
            return timeConstant() * param[2] / (param[0] * param[3]); 
        };

        inputs = () => new float[] { voltage };
        stateSpace = new FirstOrderTF(timeConstant, DCGain, integrationMethod: integrator);
    }

    public override float MotorFunction(Func<float[]> inputs, Func<float[]> parameters)
    {
        stateSpace.input = inputs()[0];
        stateSpace.Compute();

        // Apply saturation
        stateSpace.states[0, 0] = Mathf.Clamp(stateSpace.output, lowerSaturation, upperSaturation);

        return stateSpace.output;
    }
}