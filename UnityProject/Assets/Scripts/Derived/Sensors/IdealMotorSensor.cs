using System;

public class IdealMotorSensor : Sensor
{
    /// <summary>
    /// <see cref="Motor"/> object to monitor.
    /// </summary>
    public Motor motor;

    public override float[] GetOutput()
    {
        return new float[] { outputs[0]() };
    }

    protected override void Initialize()
    {
        outputs = new Func<float>[]
        {
            () => motor.MotorFunction(motor.inputs, motor.parameters),
        };
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
