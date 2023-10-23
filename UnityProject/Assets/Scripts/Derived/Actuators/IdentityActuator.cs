using System;

/// <summary>
/// Implements the identity function where output = input.
/// </summary>
public class IdentityActuator : Actuator
{
    /// <summary>
    /// The input value.
    /// </summary>
    public float input = 0;

    /// <summary>
    /// <see cref="Load"/> object that is attached to the actuator.
    /// </summary>
    public Load load;

    public override float[] GetInput() => new float[] { input };
    public override void SetInput(float[] value) => input = value[0];

    private void OnValidate()
    {
        Initialize();
    }

    private void Awake()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        if (TryGetComponent(out load))
        {
            load.rb = rb;
        }

        inputs = new Func<float>[1] { () => input };
    }

    private void OnEnable()
    {
        if (load != null)
        {
            load.AttachActuator(inputs[0]);
        }
    }

    private void OnDisable()
    {
        if (load != null)
        {
            load.DetachActuator();
        }
    }
}