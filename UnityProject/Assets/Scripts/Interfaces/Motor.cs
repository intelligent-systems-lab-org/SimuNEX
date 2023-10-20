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
    /// Inputs to the motor.
    /// </summary>
    protected Func<float>[] inputs;

    /// <summary>
    /// Parameters specific to the motor.
    /// </summary>
    protected Func<float>[] parameters;

    protected MotorFunction MF = null;

    /// <summary>
    /// The motor function (MF) that computes output values based on the provided inputs and parameters.
    /// </summary>
    /// <param name="inputs">Input values to the motors (e.g., voltage).</param>
    /// <param name="parameters">Parameters specific to the motor (e.g., back EMF constant).</param>
    /// <returns>The output angular velocity.</returns>
    public delegate float MotorFunction(Func<float>[] inputs, Func<float>[] parameters);

    private void Awake()
    {
        motorLoad = GetComponent<MotorLoad>();
        Initialize();
    }

    /// <summary>
    /// Sets up properties and defines the <see cref="MotorFunction"/> for simulation.
    /// </summary>
    protected abstract void Initialize();

    private void OnEnable()
    {
        motorLoad.AttachMotor(() => MF(inputs, parameters));
    }

    private void OnDisable()
    {
        motorLoad.DetachMotor();
    }
}
