using System;

/// <summary>
/// Implements the identity function where output = input.
/// </summary>
public class IdentityActuator : Actuator
{
    /// <summary>
    /// The input voltage.
    /// </summary>
    public float input = 0;

    public override float[] GetInput() => new float[] { input };
    public override void SetInput(float[] value) => input = value[0];

    protected override void Initialize()
    {
        inputs = new Func<float>[1] { () => input };
    }
}