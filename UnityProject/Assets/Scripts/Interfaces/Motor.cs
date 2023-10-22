using System;

/// <summary>
/// Interface for implementing motors.
/// </summary>
public abstract class Motor : Actuator
{
    /// <summary>
    /// <see cref="MotorLoad"/> object that is attached to the motor.
    /// </summary>
    public MotorLoad motorLoad;

    /// <summary>
    /// The motor function (MF) that computes output values based on the provided inputs and parameters.
    /// </summary>
    /// <param name="inputs">Input values to the motors (e.g., voltage).</param>
    /// <param name="parameters">Parameters specific to the motor (e.g., back EMF constant).</param>
    /// <returns>The output angular velocity.</returns>
    public abstract float MotorFunction(Func<float>[] inputs, Func<float>[] parameters);

    private void OnValidate()
    {
        motorLoad = GetComponent<MotorLoad>();
        if (motorLoad != null) motorLoad.rb = rb;
        Initialize();
    }

    private void OnEnable()
    {
        if (motorLoad != null) motorLoad.AttachMotor(() => MotorFunction(inputs, parameters));
    }

    private void OnDisable()
    {
        if (motorLoad != null) motorLoad.DetachMotor();
    }
}
