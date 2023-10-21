using System;

/// <summary>
/// Implements an ideal sensor that measures velocities and positions of a <see cref="RigidBody"/>.
/// </summary>
public class Ideal6DOFMotionSensor : Sensor
{
    /// <summary>
    /// Initializes sensor function.
    /// </summary>
    protected override void Initialize()
    {
        outputs = new Func<float>[]
        {
            () => rb.velocity.x, 
            () => rb.velocity.y, 
            () => rb.velocity.z,
            () => rb.angularVelocity.x, 
            () => rb.angularVelocity.y, 
            () => rb.angularVelocity.z,
            () => rb.angularPosition.x, 
            () => rb.angularPosition.y, 
            () => rb.angularPosition.z, 
            () => rb.angularPosition.w,
            () => rb.position.x, 
            () => rb.position.y, 
            () => rb.position.z
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

    public override float[] GetOutput()
    {
        return new float[]
        {
            rb.velocity.x, rb.velocity.y, rb.velocity.z,
            rb.angularVelocity.x, rb.angularVelocity.y, rb.angularVelocity.z,
            rb.angularPosition.x, rb.angularPosition.y, rb.angularPosition.z, rb.angularPosition.w,
            rb.position.x, rb.position.y, rb.position.z
        };
    }
}
