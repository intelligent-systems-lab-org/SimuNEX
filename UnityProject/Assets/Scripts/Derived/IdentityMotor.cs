using System;

/// <summary>
/// Implements the identity function where output speed = input voltage.
/// </summary>
public class IdentityMotor : Motor
{
    /// <summary>
    /// The input voltage.
    /// </summary>
    public float voltage = 0;

    protected override void Initialize()
    {
        inputs = new Func<float>[1] { () => voltage };
        MF = (inputs, parameters) => inputs[0]();
    }
}