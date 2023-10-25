using System;

/// <summary>
/// Implements an ideal sensor that measures velocities and positions of a <see cref="RigidBody"/>.
/// </summary>
public class Ideal6DOFMotionSensor : Sensor
{
    protected override void Initialize()
    {
        outputs = () => new float[]
        {
            rb.velocity.x, rb.velocity.z, rb.velocity.y,
            rb.angularVelocity.x, rb.angularVelocity.z, rb.angularVelocity.y,
            rb.angularPosition.w, rb.angularPosition.x, rb.angularPosition.z, rb.angularPosition.y,
            rb.position.x, rb.position.z, rb.position.y
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
